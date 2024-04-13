# SYS-Compulsory-Assignment


## Introduction
This repository contains the code for the compulsory assignment in the course System Integration at EASV.
The project is a Twitter clone, built on microservices, with a user service, a tweet service, and an authentication service implemented. The gateway service uses Ocelot to route requests to the correct service.


## Inter-service communication
The services were intended to communicate with each other using the AMQP protocol, with RabbitMQ as the message broker. This would have been implemented using the EasyNetQ library, which provides a simple API for RabbitMQ.

As errors were made in the design process and the services were too dependent on each other, the services are currently communicating with each other using HTTP requests. This is not ideal, as it makes the services tightly coupled and harder to scale and maintain in the long run.
With that said, the design error was not discovered until the implementation was well underway, and it was decided to continue with the HTTP-based communication for the sake of time and simplicity, rather than refactoring the entire project to use RabbitMQ.
Additionally, the design error is less likely reoccur in the future, as the lessons learned from this project will be taken into account in future projects. 

The choice of using RabbitMQ for inter-service communication would have been preferable as it is a robust and reliable message broker that is easy to set up and use. It also provides features such as message persistence and message acknowledgements, which are important for building reliable distributed systems.
More importantly over E.g. HTTP or gRPC, RabbitMQ provides a way to decouple the services from each other, which makes it easier to scale and maintain the system in the long run.



## Deployment Strategy

All microservices are dockerized and can be deployed using docker-compose.
While FaaS and PaaS are also viable deployment strategies for microservices, as they scale automatically and are easy to deploy, they are not suitable for this project because they are not as flexible as container-based deployment, along with giving up slightly too much control.
Application containers are also a possible deployment strategy for microservices, but would would mean locking one into a technology stack, which in turn would make it harder to implement services using other languages and frameworks.
The ideal choice here would be sticking with Docker and using Container-deployment, as it is the most flexible and scalable option, and it is also among the most widely used deployment strategy for microservices.
For the container-based deployment, the entire compose file can be deployed using an orchestration tool like Docker Swarm or Kubernetes, which would make it easier to scale the services and manage the deployment in the long run.


For Docker Swarm this can be accomplished with `docker stack deploy` (assuming the images are pushed to are registry such as DockerHub)

Several of the larger cloud providers also offer managed Kubernetes and Docker Swarm services, which would make it easier to deploy and manage the services in the cloud.


### Scaling
The services can be scaled horizontally by running multiple instances of each service. This can be done by running multiple containers of each service on different hosts, or by running multiple replicas of each service in a container orchestration tool like Docker Swarm or Kubernetes. This would make it easier to handle more requests and more users, and would also make the system more resilient to failures, as there would be multiple instances of each service running.

### Security
Security for the deployment strategy can be improved by using a secure registry for storing the container images, and by using TLS for communication between the services. The services can also be secured by using authentication and authorization mechanisms, such as JWT tokens and role-based access control. The services can also be secured by using firewalls and network policies to restrict access to the services from unauthorized users.

### Disaster Recovery
Disaster recovery for the deployment strategy can be improved by using backups and snapshots of the services and the data they store. The services can also be replicated across multiple availability zones or regions to make them more resilient to failures. The services can also be monitored and managed using tools like Prometheus and Grafana to detect and respond to failures quickly.