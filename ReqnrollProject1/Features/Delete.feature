Feature: Delete

Proceso de realizar Unit Testing BDD en DELETE

@tag1
Scenario: Delete Data
    Given Se selecciona el cliente a eliminar
        | Cedula     | Apellidos        | Nombres         | FechaNacimiento | Mail                   | Telefono   | Direccion | Estado |
        | 170328410 | Padilla Carvajal | Mariana Padilla | 2025-09-30      | mariapadilla@gmail.com | 0996097226 | Quito     | True |
    When Se elimina el cliente de la db
    Then El cliente ya no debe existir en la db