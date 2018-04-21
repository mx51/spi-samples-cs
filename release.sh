#!/bin/sh

VERSION=2.1.1

cd releases

cp -r ../KebabPos/bin/Debug KebabPos-$VERSION
zip -r KebabPos-2.1.1.zip KebabPos-$VERSION

cp -r ../MotelPos/bin/Debug MotelPos-$VERSION
zip -r MotelPos-2.1.1.zip MotelPos-$VERSION

cp -r ../TablePos/bin/Debug TablePos-$VERSION
zip -r TablePos-2.1.1.zip TablePos-$VERSION

cd -
