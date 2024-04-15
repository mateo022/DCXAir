class FlightInformationRequest {
    origin: string;
    destination: string;
    isOneWay: string;
    currency: string;
}

class Journey {
    origin: string;
    destination: string;
    price: number;
    flights: Flight[];
}

class Flight{
    origin: string;
    destination: string;
    price: number;
    transport: {
        flightCarrier: string;
        flightNumber: string;
    }

}

export {FlightInformationRequest, Journey, Flight}