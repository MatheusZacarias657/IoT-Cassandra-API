#!/bin/bash
set -e
echo "Build Applications.."
docker build -t topicos-api -f Topicos/Dockerfile-API .
docker build -t topicos-etl -f Topicos/Dockerfile-ETL .

echo ""
echo "Trying to up Infrastructure..."
#docker-compose --project-name topicos -f docker-compose-infra.yaml up -d

echo ""
echo "Trying to up the System..."
docker-compose --project-name topicos -f docker-compose-app.yaml up -d

