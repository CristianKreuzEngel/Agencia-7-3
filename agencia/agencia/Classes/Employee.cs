using agencia.Interfaces;

namespace agencia.Classes;

public class Employee: IPerson
{
    private string _name;
    private string _phone;
    private string _email;
    private string _address;
    private string _login;
    private string _password;
    private string _position;
    
    public string Name { get => _name; set => _name = value; }
    
    public string PhoneNumber { get => _phone; set => _phone = value; }
    
    public string Email { get => _email; set => _email = value; }
    
    public string Address { get => _address; set => _address = value; }
    
    public string Login { get => _login; }
    
    public string Password { get => _password;}
    
    public string Position { get => _position; set => _position = value; }

}