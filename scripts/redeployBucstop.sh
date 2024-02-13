#!/bin/bash

sudo docker pull $(DOCKER-REPO-PATH)/bucstop

sudo docker compose up --no-deps -d bucstop

sudo docker image prune -f