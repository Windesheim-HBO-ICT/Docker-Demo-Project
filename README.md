# Docker-Demo-Project
Deze repository wordt gebruikt in de workshop Docker. Deze workshop is te vinden in de Leerlijn-SE wiki.

# Maken van studentversie
`python parser.py -s -r uitwerkingen`

# Opdracht 1
## Om het project te starten draai het volgende commando in de root van het project
`docker build -t windesheim-docker-workshop-opdracht-1 .`
`docker run -dp 3080:3080 windesheim-docker-workshop-opdracht-1`

# Opdracht 2
## Om het project te starten draai het volgende commando in de root van het project
`docker compose watch`

## Client URL
http://localhost:4050/

## Swagger URL
http://localhost:5080/swagger/index.html

# Opdracht 3
## Om het project te starten draai het volgende commando in de root van het project
`docker compose watch`

## Applicatie 1
http://localhost:6050/

## Applicatie 2
http://localhost:7050/

# Opdracht 4
## Om het project te starten draai het volgende commando in de root van het project
`docker compose watch`

http://localhost:8050/