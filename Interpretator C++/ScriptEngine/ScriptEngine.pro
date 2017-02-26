TEMPLATE = app
CONFIG += console c++11
CONFIG -= app_bundle
CONFIG -= qt

SOURCES += main.cpp \
    Compiler/tokens.cpp \
    Compiler/parser.cpp \
    Compiler/parseiterator.cpp
