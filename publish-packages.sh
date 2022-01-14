#!/usr/bin/env bash
echo "package file"
cd src
        dotnet pack *.csproj --include-symbols -c Release --output "."
        sleep 5
        dotnet nuget push -s ${NUGET_API_URL} -k ${NUGET_KEY} "StackExchange.Redis.Extensions.Autofac.DependencyInjection.1.0.0.symbols.nupkg"
        echo "success"
        if [[ ${?} != 0  ]]
        then
            echo "falid"
            exit -1
        fi
        cd ..
