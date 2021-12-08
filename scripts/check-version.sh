#! /bin/sh

SOURCE=package.version
SOURCE_VERSION=$(curl -s https://raw.githubusercontent.com/one-click-desktop/backend-classes/$BRANCH/$SOURCE)
CURRENT_VERSION=$(cat $SOURCE)

echo Source version:    $SOURCE_VERSION
echo Current version:   $CURRENT_VERSION

if [ "$(printf '%s\n' "$CURRENT_VERSION" "$SOURCE_VERSION" | sort -Vr | head -n1)" = "$SOURCE_VERSION" ]; then
    echo "Package version needs to be higher than the one in master"
    exit 1
fi