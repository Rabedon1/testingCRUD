Feature: Insert

Proceso de realizar Unit Testing BDD en INSERT

@tag1
Scenario: Insert Data
    Given LLenar los campos del formulario
        | Cedula     | Apellidos        | Nombres         | FechaNacimiento | Mail                   | Telefono   | Direccion | Estado |
        | 170328410 | Padilla Carvajal | Mariana Padilla | 2025-09-30      | mariapadilla@gmail.com | 0996097226 | Quito     | Activo |
    When Registro de usuario en la db
    Then El resultado del registro en la db
        | Cedula     | Apellidos        | Nombres         | FechaNacimiento | Mail                   | Telefono   | Direccion | Estado |
        | 170328410 | Padilla Carvajal | Mariana Padilla | 2025-09-30      | mariapadilla@gmail.com | 0996097226 | Quito     | Activo |