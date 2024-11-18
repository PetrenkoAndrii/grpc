# Introduction
There are two small services GrpcService and HttpService.
GrpcService - API part.
HttpService - only endpoints to call gRPC services.

# External dependencies
InMemoryDatabase is used as a database.

# Build and Test
No setup steps required. It's ready to build and run.
To test application we need to run as multiple projects and send request via Swagger into GrpcService.

