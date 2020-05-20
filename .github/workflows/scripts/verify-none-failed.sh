#!/bin/bash

fail_pattern="result=\"Failed\""
results_path=${1:?"Results path argument must be provided."}
xml_files=$(find "$results_path" -maxdepth 1 -type f -name "*.xml")

for xml_file in ${xml_files[@]}; do
    echo -n "Verifying '$xml_file'.........."
    # Test if pattern found within test result xml
    if grep -q "$fail_pattern" "$xml_file"; then
        echo "failed"
        echo "Some test(s) failed in $xml_file"
        exit 1
    fi
    echo "verified"
done

echo "No tests failed"
