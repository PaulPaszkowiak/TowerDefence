# TowerDefence
TowerDefence

Aufgabe Towerdefence:

Enviroment anlegen:
- 3D Umgebung wurde mit Unity-Primitives erstellt und dann durch Asstes ersetzt die ich mit Blender erstellt habe.
- Die 3D Objekte sind einfache Low Poly Modelle und verfügen über keine sonderliche Logic.
- Enviroment-Objekte die auf dem Spielfeld sich befinden verfügen über ein EnviromentStat-Skript welches die stellen des Spielfeldes markiert, als nicht bebaubar.
  
Grid-System:
- Für das Gridsystem wurden die Skripte Grid und GridBuilder angelgt. Sie speichern und verwalten das Gridsystem und ihre enthaltenen Objekte.
- Grid liefert die Mothoden zum erstellen eines Grid sowie die Methoden um Objekte auf diesem Grid zu platzieren/speichern.
- Das Grid ist ein einfaches zwei demensionales Array, wo jeder platz im Array eine Zelle darstellt.
- Der GridBuilder ist für den Zugriff / das Bauen auf dem Grid zuständig.
- Das Platzieren von meheren Türmen / Objecten auf einer Zelle ist nicht möglich. Das gilt auch für Zellen worauf sich die Umgebung befindet.

Türme:
- Es gibt drei unterschiedliche Türme ( Normaler Tower / Stun Tower / Aoe-Tower) die jeweils Gold kosten wenn sie auf dem Grid platziert werden.
- Die Tower verfügen über eine Reichweite und Schießen in dieser auf Gegner.
- Beim Platzieren der Türme wird eine UI für den Turm gesetzt mit der man die Reichweite erkennen kann. 
- Außerdem kann über diese UI der Schaden und die Reichweite für Gold angepasst werden.
- Türme verfügen über zwei Skripte, Tower und TowerStats. Das Skript Tower enthält die Logic wie sich ein Tower verhält. 
- TowerStats enthält eigenscheifen wie Groß ein Turm ist und gibt diese beim platzieren weiter um festzustellen wieviele Zellen im Grid eingenommen werden sollen.

Gegner/Waves:
- Es erscheinen auf dem Weg Gegner, die diesen folgen, bis zum Ziel. Diese erscheinen zyklisch in einer Welle.
- Eine Welle ist eine Menge an Gegner die über eine gewisse Zeit hintereinander erscheinen.
- Für das erscheinen von Gegnern ist das Skript WaveSpawner zuständig und wie groß und welche Gegner es sind, dies wird durch die Klasse Wave definiert.
- Gegner die das Ziel erreichen ziehen dem Spieler 1 Leben ab.
- Gegner die der Spieler/ein Turm tötet geben dem Spieler Gold.
- Gegner können dem Weg folgen oder können direkt zum Ziel Fliegen.
- Wie sich ein Gegner bewegt oder fliegt und wieviel Leben/Bewegungsgeschwindigkeit etc. dieser hat, wird im Enemy-Skrip durchgeführt und definiert.

UI & Controller:
- Eine einfache UI zum Bauen und zum Anzeigen von Gold, Leben und Zeit wurde erstellt.
- Über wieviel Leben und Gold ein Spieler verfügt wird im Player-skrip festgehalten.
- Eine einfache Kamera-Steuerung wurde im CamController-Skript erstellt.
- Ein Start&Game-Over Screen wurde erstellt zum Spiel Starten,Beenden und Wiederholen.
- Ein Baumenu / Shop wurde erstellt wo er verschiedene Türme auswählen kann.
- Das Baumenu verfügt über keine Logic. Sie gibt über das ShopHandler-Skript lediglich die Informationen weiter an das GridBuilder-Skript.

Spielverlauf:
- Im Start-Screen kann der Spieler das Spiel beginnen und bekommt Informationen über die Kamerasteuerung.
- Der Spieler kann Türme aufs Feld stellen in der Bauphase. Hat er alles getan was er wollte kann er mit einem Button eine Welle Starten.
- Jede Welle hat eine feste Zeit wie lange neue Gegner erschien. Diese Zeit wird dem Spieler in der UI angezeigt.
- Sobalt die Zeit einer Welle vorrüber ist und keine Gegner mehr am leben sind kann der Spieler erneut bauen oder die nähste Welle starten.
- Nach drei Wellen, aus drei verschiedenen Gegenertypen wird die Schwierigkeit der Gegner erhöht und der verdienst an Gold pro Gegnertötung.
- Das Towerdefence Spiel kann in einer Endlosschleife gespielt werden solang der Spieler überlebt.
- Hat der Spieler kein Leben mehr, erscheint ein Game-Over Screen womit er das Spiel beenden oder erneut starten kann.


