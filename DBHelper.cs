using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using HermesDAL;

namespace Sample
{
    public class DBHelper
    {
        private readonly DBClient dbClient;

        public DBHelper()
        {
            Database.DefaultConnectionFactory = new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0");

            dbClient = new DBClient("Data Source=HermesDB.sdf");
        }


        public List<Client> GetClient(string Id)
        {
            var clients = new List<Client>();
            try
            {
                if (!string.IsNullOrEmpty(Id))
                {
                    if (dbClient.Clients.Find(Id) != null)
                        clients.Add(dbClient.Clients.Find(Id));
                }
                else
                {
                    foreach (Client item in dbClient.Clients)
                    {
                        clients.Add(item);
                    }
                }
            }
            catch (Exception e)
            {
            }
            return clients;
        }

        public bool SetClient(Client client)
        {
            try
            {
                if (client != null && client.ClientID != null)
                {
                    if (dbClient.Clients.Find(client.ClientID) != null)
                    {
                        dbClient.Clients.Find(client.ClientID).ClientInfo = client.ClientInfo;
                        dbClient.Clients.Find(client.ClientID).ClientState = client.ClientState;
                    }
                    else
                    {
                        dbClient.Clients.Add(client);
                    }
                }
                dbClient.SaveChanges();
                dbClient.Clients.Clear(); 
                return true;
            }

            catch (Exception e)
            {
                return false;
            }
        }

        public bool RemoveClient(String clientID)
        {
            try
            {
                if (clientID != null)
                {
                    //var deleteingGameServer = dbGameServer.GameServers.Find (gameServer.TurnID);
                    dbClient.Clients.Remove(dbClient.Clients.Find(clientID));
                    dbClient.SaveChanges();
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
}
}
