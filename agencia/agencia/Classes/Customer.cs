using agencia.Interfaces;

namespace agencia.Classes;

public class Customer: IPerson
{
    private string _name;
    private string _phone;
    private string _email;
    private string _address;
    
    public string Name { get => _name; set => _name = value; }
    
    public string PhoneNumber { get => _phone; set => _phone = value; }
    
    public string Email { get => _email; set => _email = value; }
    
    public string Address { get => _address; set => _address = value; }

}