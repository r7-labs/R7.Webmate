#!/bin/sh

_PWD=$(dirname "$0")
cd "$_PWD"

mono R7.Webmate.Xwt.exe $@
