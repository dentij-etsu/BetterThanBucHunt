#!/bin/bash

sudo docker pull $(DOCKER-REPO-PATH)/gamemicro

sudo docker compose up --no-deps -d micro

sudo docker image prune -f