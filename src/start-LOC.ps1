<#d:

cd D:\GitHub\clodoaldo-zeferini-enterprise\macoratti\CleanArch_CQRS_MediatR
#>

docker-compose -f compose-LOC.yml stop
docker-compose -f compose-LOC.yml down
docker-compose -f compose-LOC.yml rm

docker rmi clenarchapi
#docker rmi mysql

docker-compose -f compose-LOC.yml up -d

#cd API

#sleep 60

#dotnet tool update --global dotnet-ef

#pwd

#dotnet ef database update


