#!/bin/sh

set -e

NSTAGE=1
stage() {
  echo "============= Stage $NSTAGE - $1 ============="
  NSTAGE=$((NSTAGE+1))
}

die() {
  echo "Error: $1"
  exit 1
}


docker build -t hclogproxy:latest -f HC.LogProxy.Api/Dockerfile .
docker tag hclogproxy:latest meyhem/hclogproxy:latest

docker push meyhem/hclogproxy:latest