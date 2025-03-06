Feature: Edit

Proceso de realizar Unit Testing BDD en EDIT

@tag2
Scenario: Edit Data
    Given El usuario con Cedula "1703289023" existe en la base de datos
    When Modificar los campos del formulario
        | Cedula     | Apellidos        | Nombres         | FechaNacimiento | Mail                   | Telefono   | Direccion | Estado |
        | 1703289023 | Padilla Carvajal | Mariana Padilla | 30/09/2025      | mariapadilla@gmail.com | 0996097226 | Quito     | Activo |
    And Registro los cambios en la base de datos
        | Cedula     | Apellidos        | Nombres         | FechaNacimiento | Mail                   | Telefono   | Direccion | Estado |
        | 1703289023 | Padilla Carvajal | Mariana Padilla | 30/09/2025      | mariapadilla@newemail.com | 0996097227 | Quito     | Inactivo |
    Then El resultado de la actualización en la base de datos
        | Cedula     | Apellidos        | Nombres         | FechaNacimiento | Mail                   | Telefono   | Direccion | Estado |
        | 1703289023 | Padilla Carvajal | Mariana Padilla | 30/09/2025      | mariapadilla@newemail.com | 0996097227 | Quito     | Inactivo |
