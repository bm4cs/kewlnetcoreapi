#!/bin/sh

curl --request POST \
    --url http://localhost:9090/token \
    --header 'content-type': 'application/json' \
    --data '{"client_id": "my-client", "client_secret": "my-secret"}'