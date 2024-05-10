#!/bin/bash
set -e
echo "Build Applications.."

if [ -z "$1" ]; then
  docker build -t topicos-api -f Topicos/Dockerfile-API .
  docker build -t topicos-etl -f Topicos/Dockerfile-ETL .
else
    docker build -t topicos-etl -f Topicos/Dockerfile-$1 .
fi

echo ""
echo "Trying to up Infrastructure..."
docker-compose --project-name topicos -f docker-compose-infra.yaml up -d --no-recreate

echo ""
echo "Trying to up the System..."
docker-compose --project-name topicos -f docker-compose-app.yaml up -d

