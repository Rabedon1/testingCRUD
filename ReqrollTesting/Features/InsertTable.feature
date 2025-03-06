Feature: Insert

Proceso de realizar Unit Testing BDD en INSERT

@tag1
Scenario: Insert Data
	Given LLenar los campos del formulario
		| Cedula     | Apellidos        | Nombres         | FechaNacimiento | Mail                   | Telefono   | Direccion | Estado |
		| 1703289023 | Padilla Carvajal | Mariana Padilla | 30/09/2025      | mariapadilla@gmail.com | 0996097226 | Quito     | Activo |
	When Registro de usuario en la base de datos
		| Cedula     | Apellidos        | Nombres         | FechaNacimiento | Mail                   | Telefono   | Direccion | Estado |
		| 1703289023 | Padilla Carvajal | Mariana Padilla | 30/09/2025      | mariapadilla@gmail.com | 0996097226 | Quito     | Activo |
	Then El resultado del registro en la base de datos
		| Cedula     | Apellidos        | Nombres         | FechaNacimiento | Mail                   | Telefono   | Direccion | Estado |
		| 1703289023 | Padilla Carvajal | Mariana Padilla | 30/09/2025      | mariapadilla@gmail.com | 0996097226 | Quito     | Activo |
	