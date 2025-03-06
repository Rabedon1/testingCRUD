Feature: Login

Login hacia el sistema de automation exercises .Login

@tag1
Scenario: Usuario ingresa crendeciales incorrectas
	Given El usuario esta en la pagina del login
	When Ingresa las credenciales rabedon2@gmail.com y la contraseña "12345678"
	And Hacer click en el boton de inicio de sesión
	Then Deberia ver un mensaje de error

	