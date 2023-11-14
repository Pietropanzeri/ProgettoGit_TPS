using System.ComponentModel;

namespace Server.Controller;

public class DatabaseController
{
    private static readonly string CREATE_INGREDIENTI_TABLE = """
                                                       CREATE TABLE IF NOT EXISTS ingredienti (
                                                           ingrediente_id int PRIMARY KEY AUTO_INCREMENT,
                                                           nome varchar(16) not null,
                                                           descrizione varchar(200),
                                                           dataInizio date, 
                                                           dataFine date
                                                       );
                                                       """; 
    
    private static readonly string CREATE_RICETTE_TABLE = """
                                                        CREATE TABLE IF NOT EXISTS ricette (
                                                            ricetta_id int PRIMARY KEY AUTO_INCREMENT,
                                                            nome varchar(16) not null,
                                                            preparazione varchar(200),
                                                            tempo int,
                                                            difficoltà int CHECK (difficoltà >= 1 AND difficoltà <= 5)
                                                        );
                                                        """; 
    
    private static readonly string CREATE_FOTO_TABLE = """
                                                        CREATE TABLE IF NOT EXISTS foto (
                                                            foto_id int PRIMARY KEY AUTO_INCREMENT,
                                                            descrizione varchar(50),
                                                            foto blob,
                                                            ricetta_id int,
                                                            foreign key (ricetta_id) references ricette(ricetta_id)
                                                        );
                                                        """;
    
    private static readonly string CREATE_RICETTAINGREDIENTI_TABLE = """
                                                               CREATE TABLE RicetteIngredienti (
                                                                  ricetta_id INT not null,
                                                                  ingrediente_id INT not null,
                                                                  FOREIGN KEY (ricetta_id) REFERENCES ricette(ricetta_id),
                                                                  FOREIGN KEY (ingrediente_id) REFERENCES ingredienti(ingrediente_id),
                                                                  PRIMARY KEY(ricetta_id, ingrediente_id)
                                                              );
                                                              """;
    public DatabaseController()
    {
    }
}