#!/bin/bash

# To run: source ./init.sh 
STRIPE_SECRET_KEY="<Variable>"
STRIPE_PUB_KEY="<Variable>"
FB_APP_ID="<Variable>"
FB_APP_SECRET="<Variable>"

export ASPNETCORE_ConnectionStrings__DefaultConnection=$CONNECTION_STRING
export ASPNETCORE_Stripe__SecretKey=$STRIPE_SECRET_KEY
export ASPNETCORE_Stripe__PublishableKey=$STRIPE_PUB_KEY
export ASPNETCORE_Facebook__AppId=$FB_APP_ID
export ASPNETCORE_Facebook__AppSecret=$FB_APP_SECRET

echo "You're all set!"