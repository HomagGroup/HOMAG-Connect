using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

using HomagConnect.IntelliDivide.Contracts.Request;

namespace HomagConnect.IntelliDivide.Contracts.Extensions;

/// <summary>
/// Extensions for <see cref="OptimizationRequestResponse"/>
/// </summary>
public static class OptimizationRequestResponseExtensions
{
    /// <summary>
    /// Validates the status code.
    /// </summary>
    /// <exception cref="ValidationException">Thrown, if there are any <see cref="ValidationResult"/></exception>
    /// <exception cref="InvalidOperationException">Thrown, if there are no <see cref="ValidationResult"/>s but but the status is <see cref=" OptimizationStatus.Faulted"/>.</exception>
    public static OptimizationRequestResponse EnsureSuccessStatusCode(this OptimizationRequestResponse response)
    {
        if (response.OptimizationStatus is OptimizationStatus.Faulted)
        {
            if (response.ValidationResults != null && response.ValidationResults is { Length: > 0 })
            {
                throw new ValidationException(response.ValidationResults[0].ErrorMessage);
            }

            throw new InvalidOperationException("Request failed.");
        }

        return response;
    }

    /// <summary>
    /// Validates the status code.
    /// </summary>
    /// <exception cref="ValidationException">Thrown, if there are any <see cref="ValidationResult"/></exception>
    /// <exception cref="InvalidOperationException">Thrown, if there are no <see cref="ValidationResult"/>s but but the status is <see cref=" OptimizationStatus.Faulted"/>.</exception>
    public static async Task<OptimizationRequestResponse> EnsureSuccessStatusCodeAsync(this Task<OptimizationRequestResponse> awaitableResponse)
    {
        var response = await awaitableResponse;

        if (response.OptimizationStatus is OptimizationStatus.Faulted)
        {
            if (response.ValidationResults != null && response.ValidationResults is { Length: > 0 })
            {
                throw new ValidationException(response.ValidationResults[0].ErrorMessage);
            }

            throw new InvalidOperationException("Request failed.");
        }

        return response;
    }
}