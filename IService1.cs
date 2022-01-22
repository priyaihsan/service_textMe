using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ServiceTextMe
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {
        // sendMessage,editMessage,deleteMessage,deleteUser,viewMessage(listMessage),viewUsername(listUsername)
        [OperationContract]
        string sendMessage(string IDMessage,string Message,int idPengguna);
        /*
        [OperationContract]
        string editMessage(string IDMessage,string Message, string toUser);
        */

        [OperationContract]
        string deleteMessage(string IDMessage);
        [OperationContract]
        string deleteUser(string IDpengguna);
        [OperationContract]
        List<listMessage> listMessage(string nameUser);
        [OperationContract]
        List<listUsername> listUsername();

        //login(user dan admin) , registrasiUser ,
        [OperationContract]
        string Login(string nameUser, string passwordUser);
        [OperationContract]
        string registrasiUser(string IDUser, string nameUser, string passwordUser);



        // TODO: Add your service operations here
    }

    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    // You can add XSD files into the project. After building the project, you can directly use the data types defined there, with the namespace "ServiceTextMe.ContractType".
    [DataContract]
    public class listMessage
    {
        [DataMember]
        public string idMessage { get; set; }
        [DataMember]
        public string Message { get; set; }

    }

    public class listUsername
    {
        [DataMember]
        public string IDUser { get; set; }
        [DataMember]
        public string NameUser { get; set; }
    }

}
