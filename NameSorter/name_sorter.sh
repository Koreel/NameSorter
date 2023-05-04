#!/bin/bash

echo "Enter the file path:"
read file_path

if [[ "$OSTYPE" == "linux-gnu"* ]]; then
    # Linux
    dot_slash="./"
else
    # Windows
    dot_slash=""
fi

if [ -f "$file_path" ]; then
    echo "File found at $file_path"
    name-sorter "$dot_slash$file_path"
else
    echo "File not found at $file_path"
fi