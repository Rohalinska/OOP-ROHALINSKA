classDiagram
    class OrderService {
        +ProcessOrder(order)
    }

    class IOrderValidator {
        +Validate(order)
    }

    class IOrderRepository {
        +Save(order)
    }

    class IEmailService {
        +Send(order)
    }

    class OrderValidator
    class OrderRepository
    class EmailService

    OrderService --> IOrderValidator
    OrderService --> IOrderRepository
    OrderService --> IEmailService

    OrderValidator ..|> IOrderValidator
    OrderRepository ..|> IOrderRepository
    EmailService ..|> IEmailService
