#!/bin/bash

set -e
run_cmd="dotnet run --server.urls http://*:80"

while true; do
    >&2 echo "running application"
    exec $run_cmd
    if [[ "$?" == "0" ]]; then
        break
    fi
    sleep 5
done
