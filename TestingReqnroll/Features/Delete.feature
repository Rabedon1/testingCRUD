Feature: Delete

Proceso de realizar Unit Testing BDD en DELETE

@tag3
Scenario: Delete Data
    Given El usuario con Cedula "1703289023" existe en la base de datos
    When Eliminar el registro del usuario
    Then Verificar que el usuario con Cedula "1703289023" no exista en la base de datos
