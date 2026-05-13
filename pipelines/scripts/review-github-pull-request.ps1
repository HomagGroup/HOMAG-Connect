Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

function Get-EnvironmentVariable {
    param(
        [Parameter(Mandatory = $true)]
        [string]$Name,
        [string]$DefaultValue = ''
    )

    $value = [Environment]::GetEnvironmentVariable($Name)
    if ([string]::IsNullOrWhiteSpace($value))
    {
        return $DefaultValue
    }

    return $value.Trim()
}

function Get-RequiredEnvironmentVariable {
    param(
        [Parameter(Mandatory = $true)]
        [string]$Name
    )

    $value = Get-EnvironmentVariable -Name $Name
    if ([string]::IsNullOrWhiteSpace($value))
    {
        throw "Environment variable '$Name' is required."
    }

    return $value
}

function Get-GitHubRepositoryInfo {
    param(
        [Parameter(Mandatory = $true)]
        [string]$RepositoryUri
    )

    $normalizedRepositoryUri = $RepositoryUri.Trim()
    if ($normalizedRepositoryUri.EndsWith('.git', [System.StringComparison]::OrdinalIgnoreCase))
    {
        $normalizedRepositoryUri = $normalizedRepositoryUri.Substring(0, $normalizedRepositoryUri.Length - 4)
    }

    if ($normalizedRepositoryUri -match '^https://github\.com/(?<owner>[^/]+)/(?<repo>[^/]+)$')
    {
        return @{
            Owner = $Matches.owner
            Repository = $Matches.repo
        }
    }

    if ($normalizedRepositoryUri -match '^git@github\.com:(?<owner>[^/]+)/(?<repo>[^/]+)$')
    {
        return @{
            Owner = $Matches.owner
            Repository = $Matches.repo
        }
    }

    throw "Repository URI '$RepositoryUri' is not a supported GitHub repository URI."
}

function Invoke-GitHubApi {
    param(
        [Parameter(Mandatory = $true)]
        [ValidateSet('Get', 'Post', 'Patch')]
        [string]$Method,

        [Parameter(Mandatory = $true)]
        [string]$Uri,

        [object]$Body = $null
    )

    $headers = @{
        Accept = 'application/vnd.github+json'
        Authorization = "Bearer $(Get-RequiredEnvironmentVariable -Name 'GITHUB_PAT')"
        'User-Agent' = 'HOMAG-Connect-Azure-DevOps-PR-Review'
        'X-GitHub-Api-Version' = '2022-11-28'
    }

    $invokeParameters = @{
        Method = $Method
        Uri = $Uri
        Headers = $headers
    }

    if ($null -ne $Body)
    {
        $invokeParameters.ContentType = 'application/json'
        $invokeParameters.Body = $Body | ConvertTo-Json -Depth 10
    }

    return Invoke-RestMethod @invokeParameters
}

function Get-GitHubPagedResults {
    param(
        [Parameter(Mandatory = $true)]
        [string]$Uri
    )

    $results = @()
    $page = 1

    while ($true)
    {
        $separator = if ($Uri.Contains('?')) { '&' } else { '?' }
        $pagedUri = "$Uri${separator}page=$page"
        $pageResults = @(Invoke-GitHubApi -Method Get -Uri $pagedUri)
        $results += $pageResults

        if ($pageResults.Count -lt 100)
        {
            break
        }

        $page++
    }

    return $results
}

function Test-IsGeneratedFile {
    param(
        [Parameter(Mandatory = $true)]
        [string]$Path
    )

    $generatedPatterns = @(
        '*.designer.cs',
        '*.generated.cs',
        '*.g.cs',
        '*.g.i.cs',
        '*.min.js',
        '*.min.css',
        '*.map',
        'package-lock.json',
        '*.snap',
        '*.resx'
    )

    foreach ($generatedPattern in $generatedPatterns)
    {
        if ($Path -like $generatedPattern)
        {
            return $true
        }
    }

    return $false
}

function Format-MarkdownTableCell {
    param(
        [AllowNull()]
        [string]$Value
    )

    if ([string]::IsNullOrWhiteSpace($Value))
    {
        return ''
    }

    return ($Value -replace '\|', '\|' -replace "`r?`n", '<br/>')
}

$buildReason = Get-EnvironmentVariable -Name 'BUILD_REASON'
$repositoryProvider = Get-EnvironmentVariable -Name 'BUILD_REPOSITORY_PROVIDER'
$pullRequestNumber = Get-EnvironmentVariable -Name 'SYSTEM_PULLREQUEST_PULLREQUESTNUMBER'

if ($buildReason -ne 'PullRequest')
{
    Write-Host 'Skipping pull request review because this is not a pull request run.'
    exit 0
}

if ($repositoryProvider -ne 'GitHub')
{
    Write-Host "Skipping pull request review because repository provider '$repositoryProvider' is not GitHub."
    exit 0
}

if ([string]::IsNullOrWhiteSpace($pullRequestNumber))
{
    Write-Host 'Skipping pull request review because no pull request number is available.'
    exit 0
}

$repositoryInfo = Get-GitHubRepositoryInfo -RepositoryUri (Get-RequiredEnvironmentVariable -Name 'BUILD_REPOSITORY_URI')
$apiBaseUri = "https://api.github.com/repos/$($repositoryInfo.Owner)/$($repositoryInfo.Repository)"
$pullRequestUri = "$apiBaseUri/pulls/$pullRequestNumber"
$pullRequestFilesUri = "$apiBaseUri/pulls/$pullRequestNumber/files?per_page=100"
$issueCommentsUri = "$apiBaseUri/issues/$pullRequestNumber/comments?per_page=100"
$marker = '<!-- homag-connect-ado-pr-review -->'

Write-Host "Retrieving pull request #$pullRequestNumber from $($repositoryInfo.Owner)/$($repositoryInfo.Repository)..."
$pullRequest = Invoke-GitHubApi -Method Get -Uri $pullRequestUri
$pullRequestFiles = @(Get-GitHubPagedResults -Uri $pullRequestFilesUri)
$issueComments = @(Get-GitHubPagedResults -Uri $issueCommentsUri)

$totalAdditions = @($pullRequestFiles | Measure-Object -Property additions -Sum)[0].Sum
if ($null -eq $totalAdditions) { $totalAdditions = 0 }
$totalDeletions = @($pullRequestFiles | Measure-Object -Property deletions -Sum)[0].Sum
if ($null -eq $totalDeletions) { $totalDeletions = 0 }
$totalChangedLines = $totalAdditions + $totalDeletions

$interestingFiles = @($pullRequestFiles | Where-Object { -not (Test-IsGeneratedFile -Path $_.filename) })
$codeExtensions = @('.cs', '.csproj', '.props', '.targets', '.ps1', '.json', '.yml', '.yaml', '.ts', '.tsx', '.js', '.jsx')
$codeFiles = @($interestingFiles | Where-Object {
    $extension = [System.IO.Path]::GetExtension([string]$_.filename)
    $codeExtensions -contains $extension.ToLowerInvariant()
})
$testFiles = @($interestingFiles | Where-Object {
    $_.filename -match '(^|/)(test|tests?)/' -or
    $_.filename -match '\.Tests?\.' -or
    $_.filename -match 'Tests?\.csproj$'
})
$filesWithoutPatch = @($pullRequestFiles | Where-Object { -not $_.PSObject.Properties.Match('patch') -or [string]::IsNullOrWhiteSpace([string]$_.patch) })

$findings = New-Object System.Collections.Generic.List[string]
if ($pullRequestFiles.Count -gt 40)
{
    $findings.Add("Large pull request detected with $($pullRequestFiles.Count) changed files. Consider splitting it into smaller PRs if possible.")
}

if ($totalChangedLines -gt 1500)
{
    $findings.Add("Large diff detected with $totalChangedLines changed lines. Focused review may be harder and risk missing issues.")
}

if ($codeFiles.Count -gt 0 -and $testFiles.Count -eq 0)
{
    $findings.Add('Code-oriented changes were detected but no matching test changes were found.')
}

if ($filesWithoutPatch.Count -gt 0)
{
    $findings.Add("$($filesWithoutPatch.Count) changed file(s) are binary or too large for inline diff inspection via the GitHub files API.")
}

if (($pullRequestFiles | Where-Object { $_.filename -like '*.yml' -or $_.filename -like '*.yaml' }).Count -gt 0)
{
    $findings.Add('Pipeline or YAML configuration changes detected. Validate conditions, secrets handling, and branch filters carefully.')
}

$topFiles = @($pullRequestFiles | Select-Object -First 15)
$commentLines = New-Object System.Collections.Generic.List[string]
$commentLines.Add($marker)
$commentLines.Add('## Automated pull request review summary')
$commentLines.Add('')
$commentLines.Add('This comment is maintained by the Azure DevOps review job and is updated in place to reduce noise.')
$commentLines.Add('')
$commentLines.Add('| Metric | Value |')
$commentLines.Add('| --- | --- |')
$commentLines.Add("| Pull request | #$pullRequestNumber |")
$commentLines.Add("| Title | $(Format-MarkdownTableCell -Value $pullRequest.title) |")
$commentLines.Add("| Files changed | $($pullRequestFiles.Count) |")
$commentLines.Add("| Additions | $totalAdditions |")
$commentLines.Add("| Deletions | $totalDeletions |")
$commentLines.Add("| Review scope | GitHub PR metadata and changed files API |")
$commentLines.Add('')

if ($findings.Count -eq 0)
{
    $commentLines.Add('### Findings')
    $commentLines.Add('')
    $commentLines.Add('- No structural concerns were detected by the lightweight automated heuristics for this PR.')
}
else
{
    $commentLines.Add('### Findings')
    $commentLines.Add('')
    foreach ($finding in $findings)
    {
        $commentLines.Add("- $finding")
    }
}

if ($topFiles.Count -gt 0)
{
    $commentLines.Add('')
    $commentLines.Add('### Top changed files')
    $commentLines.Add('')
    $commentLines.Add('| File | Status | + | - |')
    $commentLines.Add('| --- | --- | ---: | ---: |')

    foreach ($topFile in $topFiles)
    {
        $commentLines.Add("| $(Format-MarkdownTableCell -Value ([string]$topFile.filename)) | $([string]$topFile.status) | $([int]$topFile.additions) | $([int]$topFile.deletions) |")
    }
}

$commentLines.Add('')
$commentLines.Add('> This review is generated from GitHub pull request metadata and diff statistics. It is intended as an advisory summary and does not replace human review.')
$commentBody = $commentLines -join [Environment]::NewLine

$existingComment = $issueComments |
    Where-Object { $_.body -like "*$marker*" } |
    Sort-Object -Property updated_at -Descending |
    Select-Object -First 1

if ($null -ne $existingComment -and [string]$existingComment.body -eq $commentBody)
{
    Write-Host 'Existing review comment is already up to date.'
    exit 0
}

if ($null -ne $existingComment)
{
    $updateCommentUri = "$apiBaseUri/issues/comments/$($existingComment.id)"
    Write-Host "Updating existing review comment $($existingComment.id)..."
    Invoke-GitHubApi -Method Patch -Uri $updateCommentUri -Body @{ body = $commentBody } | Out-Null
}
else
{
    $createCommentUri = "$apiBaseUri/issues/$pullRequestNumber/comments"
    Write-Host 'Creating new review comment...'
    Invoke-GitHubApi -Method Post -Uri $createCommentUri -Body @{ body = $commentBody } | Out-Null
}

Write-Host 'GitHub pull request review comment updated successfully.'
