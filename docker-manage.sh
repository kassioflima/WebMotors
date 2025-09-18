#!/bin/bash

# WebMotors Docker Compose Management Script

echo "=== WebMotors Docker Compose Management ==="

case "$1" in
    "up")
        echo "Starting WebMotors services..."
        docker-compose --env-file docker.env up -d
        echo "Services started successfully!"
        echo "API: http://localhost:6000"
        echo "Swagger: http://localhost:6000/swagger"
        echo "SQL Server: localhost:1433"
        ;;
    "down")
        echo "Stopping WebMotors services..."
        docker-compose down
        echo "Services stopped successfully!"
        ;;
    "build")
        echo "Building WebMotors API..."
        docker-compose build webmotors.api
        echo "Build completed!"
        ;;
    "logs")
        echo "Showing logs..."
        docker-compose logs -f
        ;;
    "restart")
        echo "Restarting WebMotors services..."
        docker-compose down
        docker-compose --env-file docker.env up -d
        echo "Services restarted successfully!"
        ;;
    "clean")
        echo "Cleaning up containers and volumes..."
        docker-compose down -v
        docker system prune -f
        echo "Cleanup completed!"
        ;;
    *)
        echo "Usage: $0 {up|down|build|logs|restart|clean}"
        echo ""
        echo "Commands:"
        echo "  up      - Start all services"
        echo "  down    - Stop all services"
        echo "  build   - Build the API image"
        echo "  logs    - Show logs from all services"
        echo "  restart - Restart all services"
        echo "  clean   - Stop services and clean up volumes"
        exit 1
        ;;
esac
