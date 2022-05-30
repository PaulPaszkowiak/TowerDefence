# TowerDefence
TowerDefence

Aufgabe Towerdefence:

Enviroment anlegen:
- 3D Umgebung wurde mit Unity-Primitives erstellt und dann durch Asstes ersetzt die ich mit Blender erstellt habe
- Die 3D Objekte sind einfache Low Poly Modelle und verfügen über keine sonderliche Logic.

Grid-System:
- Für das Gridsystem wurden die Skripte Grid und GridBuilder angelgt. Sie speichern und verwalten das Gridsystem und ihre enthaltenen Objekte.
- Grid liefert die Mothoden zum erstellen eines Grid sowie die Methoden um Objekte auf diesem grid zum platzieren/speichern.
- Das Grid ist ein einfaches zwei demensionales Array wo jeder platz im Array eine Zelle darstellt
- Der GridBuilder ist für den Zugriff / das Bauen auf dem Grid zuständig.
- Das Platzieren von meheren Türmen / Objecten auf eine Zelle ist nicht möglich. Das gilt auch für Zellen worauf sich die Umgebung befindet.

Türme:
- Es gibt drei unterschiedliche Türme ( Normaler Tower / Stun Tower / Aoe-Tower) die jeweils Gold kosten wenn sie auf dem Grid platziert werden.
- Die Tower verfügen über eine Reichweite und Schießen in dieser auf Gegner.
- Beim Platzieren der Türme wird eine UI für den Turm gesetzt mit der man die Reichweite erkennen kann. 
- Außerdem kann über diese UI der Schaden und die Reichweite für Gold angepasst werden.
- Türme verfügen über zwei Skripte, Tower und TowerStats. Das Skript Tower enthält die Logic wie sich ein Tower verhält. 
- TowerStats enthält eigenscheifen wie Groß ein Turm ist und gibt diese beim platzieren weiter um festzustellen wieviele Zellen im Grid eingenommen werden sollen.

Gegner/Waves:
- Es erscheinen auf dem Weg Gegner die diesen verfolgen bis zum Ziel. Diese erscheinen zyklisch in einer Welle.
- Eine Welle ist eine Menge an Gegner die über eine gewisse Zeit hintereinander erscheinen.
- Für das erscheinen von Gegnern ist das Skript WaveSpawner zuständig und wie groß und welche Gegner es sind, dies wird durch die Klasse Wave definiert.
- Gegner die das Ziel erreichen ziehen dem Spieler 1 Leben ab.
- Gegner die der Spieler/ein Turm tötet geben dem Spieler Gold.
- Gegner können dem Weg folgen oder können direkt zum Ziel Fliegen.
- Wie sich ein Gegner bewegt oder fliegt und wieviel Leben/Bewegungsgeschwindigkeit etc. dieser hat wird im Enemy-Skrip durchgeführt und definiert.
- Über wieviel Leben und Gold ein Spieler verfügt wird im Player-skrip festgehalten.

UI & Controller:
- Eine einfache UI zum Bauen und zum Anzeigen von Gold, Leben und Zeit wurde erstellt.
- Eine einfache Kamera-Steuerung wurde erstellt.
- Ein Start&Game-Over Screen wurde erstellt zum Spiel Starten,Beenden und Wiederholen.




