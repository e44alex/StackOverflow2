#!/bin/bash

set -e
run_cmd="dotnet run --server.urls http://*:80"

until dotnet ef database update; do
>&2 echo "SQL Server is strating up"
sleep 1
dotnet

>&2 echo "SQL Server is up - executing command"
exec $run_cmd