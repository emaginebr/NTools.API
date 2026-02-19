#!/bin/bash
docker stop ztools-api1
docker rm ztools-api1
cd ./Backend/zTools
docker build -t ztools-api -f ./zTools.API/Dockerfile .
docker run --name ztools-api1 -e ASPNETCORE_URLS="https://+" -e ASPNETCORE_HTTPS_PORTS=443 --network docker-network ztools-api &
docker ps
