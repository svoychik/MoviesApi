# Project to demonstrate how to build API with .Net Core & API Gateway & serverless framework

The project is configured for deployment in eu-central-1 AWS
To deploy project to your own AWS account generate your own API Keys and set them up with `aws configure`

## Deployment

Prerequisites: AWS keys

```
npm i
npm run build-deploy

# seed dynamodb db Movies table with some dummy data
chmod +wrx ./seed.sh
./seed.sh
```

## Prerequisites to install

- [NodeJS](https://nodejs.org/en/)
- [Serverless Framework CLI](https://serverless.com)
- [.NET Core 6](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
- [AWS-Lambda-DotNet](https://github.com/aws/aws-lambda-dotnet)
- [AWS-Lambda-DotNet](https://github.com/aws/aws-lambda-dotnet)
- [Visual Studio Code](https://code.visualstudio.com/)
- [VS Code Extension Rest Client](https://marketplace.visualstudio.com/items?itemName=humao.rest-client)
Verify that everything is installed (copy & paste)

```bash
# package manager for nodejs
npm -v
# serverless framework cli > 1.5
sls -v
# dotnet (cli) > 6.0
dotnet --version
```

### After successful deployment you can the similar output
<pre>
Service Information
service: myService
stage: dev
region: <b>us-east-1</b>
stack: myService-dev
resources: 10
api keys:
  None
endpoints:
  GET - <b>endpointUrl --> https://{api}.execute-api.us-east-1.amazonaws.com/dev/hello</b>
functions:
  hello: myService-dev-hello
layers:
  None

</pre>

### Test endpoint after deployment

3 options:

- [Use postman as UI Tool](https://www.getpostman.com/)
- Use curl
- Use VS Code Rest Client extensions and run scripts from `/requests/*.http` folder

Use the **endpointUrl** from up above.

## Destroy the stack in the cloud

```bash
sls remove
```

###### I deployed the solution but I get back an http 500 error

1. Check Cloudwatch Logs in AWS - the issue should be describe there.
2. For a successful response of function getquerystring the querystringParameter **foo** must be inserted

###### How can I change the lambda region or stack name

Please have a look to the serverless guideline: <https://serverless.com/framework/docs/providers/aws/guide/deploying/>
