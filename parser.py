#!/usr/bin/python3

import os
from shutil import copytree, ignore_patterns, rmtree
import subprocess
import sys, getopt
import argparse
import logging
import os

info = logging.info
# logging.basicConfig(filename='example.log',level=logging.DEBUG)
logging.basicConfig(level=logging.DEBUG)

# Let op: Dit programma maakt gebruik van sed. Dit is een linux commando. Op windows werkt dit niet.

# Start van het programma: $ python main.py
# Parameters:
# --help, -h
# --src, -r optioneel default ./uitwerking
# --dest, -d optioneel default ./
# --proefversie, -p een proefopdracht
# --studentversie, -s de studentversie van het practicum
# --c, -c, het gebruiken van een config bestand: .prep-cfg

# Voorbeeld uitvoeren van studentversie:
# $ python labs/main.py -s -r labs/js/02_js_classes/uitwerking -d labs/js/02_js_classes

# Voorbeeld configuratie, .prep-cfg:
# excluded_files: gulpfile.js/tasks/js.js
# excluded_dirs: js,test

# Voorbeeld uitvoeren van studentversie met configuratie
# $ python labs/main.py -s -c -r labs/js/02_js_classes/uitwerking -d labs/js/02_js_classes

def studentversie(src, dest, cfg):
    info("student versie klaarzetten")
    map = os.path.join(dest, 'studentversie')
    copytree(src, map,
             ignore=ignore_patterns('*.docx', '*.zip', 'tmp*', 'node_modules', 'nakijken*', '*.tmp', '.git', '.idea', 'assets',
                                    'dist', 'dest'))

    if(cfg and cfg.get('excluded_files')):
        for ex in cfg.get('excluded_files'):
            os.remove(os.path.join(dest, 'studentversie', ex))

    if(cfg and cfg.get('excluded_dirs')):
        for ex in cfg.get('excluded_dirs'):
            rmtree(os.path.join(dest, 'studentversie', ex))

    for root, dirs, files in os.walk(map, topdown=False):

        for name in files:
            print(os.path.join(root, name))
            
            cmdJS = 'sed -i "/\/\/#student-start/,/\/\/#student-end/c\//student uitwerking" ' + os.path.join(root, name)
            cmdCSS = 'sed -i "/\/*#student-start/,/\/*#student-end/c\/*student uitwerking*/" ' + os.path.join(root, name)

            # New command for Dockerfiles and docker-compose.yaml
            if name == "Dockerfile" or name == "docker-compose.yaml":
                cmdDocker = 'sed -i "/#student-start/,/#student-end/c\#student uitwerking" ' + os.path.join(root, name)
                subprocess.call(cmdDocker, shell=True)
            else:
                # Execute JS and CSS commands only if the file is not a Dockerfile or docker-compose.yaml
                subprocess.call(cmdJS, shell=True)
                subprocess.call(cmdCSS, shell=True)

# De proefversie vereist dat elke opdracht in een map staat 'opdracht*'.
def proefversie(src, dest, cfg):
    info('proefversie klaarzetten')

    map = os.path.join(dest, 'proefversie')
    copytree(src, map,
             ignore=ignore_patterns('*.docx', '*.zip', 'tmp*', 'node_modules', 'nakijken*', '*.tmp', '.git', '.idea', 'assets',
                                    'dist', 'dest', 'opdracht*'))

    copytree(os.path.join(src, 'opdracht-proef'), os.path.join(map, 'opdracht-proef'))


def examples(src, dest):
    info('examples kopieren')
    info(src)
    copytree(src, dest,
             ignore=ignore_patterns('tmp*', 'node_modules', '*.tmp', '.git', '.idea',
                                    'dist', 'dest'))

def main(argv):
    parser = argparse.ArgumentParser()
    parser.add_argument("-r", "--src", help="de src map van het project")
    parser.add_argument("-d", "--dest", help="de dest map van het project")
    parser.add_argument("-p", "--proefversie",
                        help="het klaarzetten van een studentsetup project zonder opgaven",
                        action="store_true"
                        )
    parser.add_argument("-s", "--studentversie",
                        help="het klaarzetten van een studentsetup voor het practicum met opgaven",
                        action="store_true"
                        )
    parser.add_argument("-e", "--examples",
                        help="het klaarzetten van de examples in de studenten map/repository",
                        action="store_true"
                        )
    parser.add_argument("-c", "--config",
                        help="het gebruiken van een config bestand: .prep-cfg",
                        action="store_true")
    args = parser.parse_args()

    src = './uitwerking'
    dest = './' # op die locatie worden de mappen proefversie en studentversie gemaakt

    if( args.src):
        src = getattr(args, 'src')

    if (args.dest):
        dest = getattr(args, 'dest')

    if(args.examples):
        if(not args.src):
            src = './examples'
        if(not args.dest):
            print('Geef een dest op voor de examples. Gebruik -h voor meer info.')
            return

    if(not args.dest and args.examples):
            print('Geef een src op voor de examples')
            return


    print('default src: ' + src)
    print('default dest: ' + dest)
    cfg = {}
    if(args.config):
        if(not os.path.exists(os.path.join(src, '.prep-cfg'))):
            print('Bestand .prep-cfg bestaat niet. Voeg deze toe in dezelfde map waar dit script runt.')
            return

        with open(os.path.join(src, ".prep-cfg"), "r") as file:

            readline = file.read().splitlines()
            for l in readline:
                print(l)
                key = l.split()[0]
                key = key[0: -1]
                value = None
                if(key == 'excluded_files' or key == 'excluded_dirs'):
                    value = l.split()[1].split(',')
                    print(value)
                else:
                    value = l.split()[1]
                cfg[key] = value

    for arg in vars(args):
        if arg == 'proefversie' and getattr(args, arg):
            if(cfg and cfg.has_key('excluded_files')):
                print('Configuratie parameter excluded_files niet geimplementeerd voor de proefversie')
            proefversie(src, dest, cfg)
        elif arg == 'studentversie' and getattr(args, arg):
            studentversie(src, dest, cfg)
        elif arg == 'examples' and getattr(args, arg):
                    examples(src, dest)

    info('de bestanden zijn beschikbaar: ' + dest)
    sys.exit()

    # cwd = os.getcwd()
    # print(cwd)

if __name__ == '__main__':
    main(sys.argv)
