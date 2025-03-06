Feature: Edit

Proceso de realizar Unit Testing BDD en Editar

@tag1
Scenario: Edit Data
    Given Editar los campos del formulario
        | Cedula     | Apellidos        | Nombres         | FechaNacimiento | Mail                   | Telefono   | Direccion | Estado |
        | 170328410 | Padilla Carvajal | Mariana Padilla | 2025-09-30      | mariapadilla@gmail.com | 0996097226 | Quito     | Activo |
    When Actualizar usuario en la db
    Then El resultado de la actualización en la db
        | Cedula     | Apellidos        | Nombres         | FechaNacimiento | Mail                   | Telefono   | Direccion | Estado |
        | 170328410 | Padilla Carvajal | Mariana Padilla | 2025-09-30      | mariapadilla@gmail.com | 0996097226 | Quito     | Activo |