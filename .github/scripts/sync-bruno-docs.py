import os
import re
from pathlib import Path

# Pfade zu Ihren Ordnern anpassen
API_DOCS_DIR = Path("Documentation/API")
BRUNO_DIR = Path("Documentation/Bruno")

def sync_docs():
    # Gehe durch alle .bru Dateien im Bruno-Ordner (auch in Unterordnern)
    for bru_file in BRUNO_DIR.rglob("*.bru"):
        
        # Angenommen, die MD-Datei heißt genauso wie die BRU-Datei
        # z.B. GetMachineData.bru sucht nach GetMachineData.md
        md_file = API_DOCS_DIR / f"{bru_file.stem}.md"
        
        if md_file.exists():
            # Markdown-Inhalt lesen
            with open(md_file, "r", encoding="utf-8") as f:
                md_content = f.read().strip()
            
            # Bruno-Datei lesen
            with open(bru_file, "r", encoding="utf-8") as f:
                bru_content = f.read()
            
            # Den neuen docs-Block vorbereiten
            new_docs_block = f"docs {{\n{md_content}\n}}"
            
            # Prüfen, ob bereits ein docs-Block existiert
            if re.search(r'^docs\s*\{.*?^\}', bru_content, flags=re.DOTALL | re.MULTILINE):
                # Ersetze existierenden Block
                new_bru_content = re.sub(r'^docs\s*\{.*?^\}', new_docs_block, bru_content, flags=re.DOTALL | re.MULTILINE)
            else:
                # Wenn nicht, hänge den neuen Block ans Ende der Datei an
                new_bru_content = bru_content.rstrip() + f"\n\n{new_docs_block}\n"
            
            # Nur schreiben, wenn sich etwas geändert hat
            if new_bru_content != bru_content:
                with open(bru_file, "w", encoding="utf-8") as f:
                    f.write(new_bru_content)
                print(f"✅ Aktualisiert: {bru_file.name}")

if __name__ == "__main__":
    sync_docs()
