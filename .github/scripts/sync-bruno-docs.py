"""
This script synchronizes external Markdown documentation with Bruno API request files.
It searches for .bru files in the Bruno directory, looks for a matching .md file
in the API documentation directory, and injects the Markdown content into the 
'docs { ... }' block of the corresponding .bru file.
"""

import os
import re
from pathlib import Path

# Determine the repository root dynamically based on the script's location.
# __file__ represents '.github/scripts/sync-bruno-docs.py'
# .parent.parent.parent navigates three levels up to the repository root.
REPO_ROOT = Path(__file__).resolve().parent.parent.parent

# Define the absolute paths to the documentation directories
API_DOCS_DIR = REPO_ROOT / "Documentation" / "API"
BRUNO_DIR = REPO_ROOT / "Documentation" / "Bruno"

def sync_docs():
    """
    Iterates through all .bru files, checks for a corresponding .md file,
    and updates the 'docs' block inside the .bru file if necessary.
    """
    # Recursively find all .bru files in the Bruno directory and its subdirectories
    for bru_file in BRUNO_DIR.rglob("*.bru"):
        
        # Assume the Markdown file has the exact same name as the Bruno file (e.g., GetMachineData.md)
        md_file = API_DOCS_DIR / f"{bru_file.stem}.md"
        
        if md_file.exists():
            # Read the content of the Markdown file
            with open(md_file, "r", encoding="utf-8") as f:
                md_content = f.read().strip()
            
            # Read the current content of the Bruno file
            with open(bru_file, "r", encoding="utf-8") as f:
                bru_content = f.read()
            
            # Prepare the new docs block formatted for Bruno
            new_docs_block = f"docs {{\n{md_content}\n}}"
            
            # Check if a 'docs { ... }' block already exists in the .bru file
            # We use DOTALL to match across multiple lines and MULTILINE for line anchors
            if re.search(r'^docs\s*\{.*?^\}', bru_content, flags=re.DOTALL | re.MULTILINE):
                # Replace the existing docs block with the new one
                new_bru_content = re.sub(
                    r'^docs\s*\{.*?^\}', 
                    new_docs_block, 
                    bru_content, 
                    flags=re.DOTALL | re.MULTILINE
                )
            else:
                # If no docs block exists, append the new block to the end of the file
                new_bru_content = bru_content.rstrip() + f"\n\n{new_docs_block}\n"
            
            # Write the changes back to the file only if the content has actually changed
            # This prevents unnecessary Git commits when the documentation is already up to date
            if new_bru_content != bru_content:
                with open(bru_file, "w", encoding="utf-8") as f:
                    f.write(new_bru_content)
                print(f"✅ Updated: {bru_file.name}")

if __name__ == "__main__":
    sync_docs()
