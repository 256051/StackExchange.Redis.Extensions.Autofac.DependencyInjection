#!/usr/bin/env bash
echo "package file"
cd src
for directory in *; do 
    if [[ -d ${directory} ]]; then
        cd ${directory}
        dotnet pack *.csproj --include-symbols -c Release --output "."
        sleep 5
        dotnet nuget push -s ${NUGET_API_URL} -k ${NUGET_KEY} "${directory}.${TAG}.symbols.nupkg"
        echo "success"
        if [[ ${?} != 0  ]]
        then
            echo "falid"
            exit -1
        fi
        cd ..
    fi
done
