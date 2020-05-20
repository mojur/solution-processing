#!/bin/bash

base_path=${1:-.} # Default to current directory
base_path=${base_path%/} # Trim trailing slash
curr_pkg_path="$base_path"
new_pkg_path="$base_path/Packages/package"

# Get package contents into array
pkg_files=$(find "$curr_pkg_path" -maxdepth 1 -not -path "$curr_pkg_path" -not -path "$base_path" -not -path "$base_path/.github")

# Create project skeleton
echo "Creating project..."
mkdir -vp "$base_path/Assets" "$base_path/ProjectSettings" "$new_pkg_path"

# Move package into project
echo "Moving package into new project..."
for file in ${pkg_files[@]}; do
    mv -v "$file" "$new_pkg_path"
done

echo "Project created at '$base_path' with package at '$new_pkg_path'"
