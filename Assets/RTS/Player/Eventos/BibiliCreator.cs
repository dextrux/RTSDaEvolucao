using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class BibiliCreator : MonoBehaviour
{
    public GameObject bibliPrefab;
    public GameObject playerPopulation;
    private Vector3 _instantiatePosition;
    private Quaternion _instantiateRotation;
    string[] names = {
    "Alice", "Bob", "Charlie", "Diana", "Eve", "Frank", "Grace", "Hannah", "Isaac", "Jack", "Karen", "Liam", "Mia", "Noah", "Olivia", "Paul", "Quinn", "Ruby", "Sophia", "Thomas", "Uma", "Violet", "William", "Xander", "Yara", "Zoe",
    "Aaron", "Beth", "Caleb", "Daniel", "Ethan", "Faith", "George", "Hazel", "Ivy", "James", "Katie", "Lucas", "Megan", "Nathan", "Oscar", "Penny", "Quincy", "Rose", "Samuel", "Tina", "Ursula", "Victor", "Wendy", "Xenia", "Yvonne", "Zachary",
    "Adam", "Bella", "Chris", "David", "Elena", "Felix", "Gina", "Harry", "Iris", "Jacob", "Kim", "Lily", "Mason", "Nina", "Owen", "Peter", "Quella", "Ryan", "Sara", "Tony", "Una", "Vince", "Wes", "Xiomara", "Yosef", "Zara",
    "Alvin", "Brenda", "Carl", "Donna", "Emily", "Freddy", "Gloria", "Henry", "Isabel", "Justin", "Kelly", "Leo", "Monica", "Nick", "Ophelia", "Paula", "Quinn", "Randy", "Selena", "Tim", "Uri", "Valerie", "Wyatt", "Xena", "Yasmin", "Zeke",
    "Ava", "Blake", "Claire", "Dean", "Ella", "Finn", "Gabriel", "Hope", "Isaiah", "Julian", "Kayla", "Liam", "Madison", "Nolan", "Olga", "Phoebe", "Ralph", "Sophie", "Tyler", "Vera", "Wesley", "Xander", "Yara", "Zach",
    "Abigail", "Ben", "Cameron", "Daisy", "Eva", "Flynn", "George", "Heidi", "Ian", "Jayden", "Katie", "Logan", "Maria", "Neil", "Olivia", "Parker", "Quinn", "Rebecca", "Scarlett", "Theo", "Ulysses", "Victoria", "Wayne", "Ximena", "Yusuf", "Zoe",
    "Austin", "Brianna", "Connor", "Derek", "Erin", "Finn", "Gemma", "Holly", "Ivy", "Joshua", "Kylie", "Leo", "Maya", "Nate", "Opal", "Peyton", "Rachel", "Shane", "Tessa", "Vince", "Wyatt", "Xia", "Yvette", "Zane",
    "Amy", "Brian", "Chloe", "Dylan", "Emily", "Finn", "Grace", "Harper", "Ian", "Jonathan", "Katherine", "Lily", "Mason", "Nina", "Olivia", "Perry", "Quinn", "Ronald", "Savannah", "Trevor", "Uma", "Victor", "Will", "Xavier", "Yasmine", "Zane",
    "Alex", "Brittany", "Chase", "Diana", "Elijah", "Faith", "Gavin", "Haley", "Isabella", "John", "Katie", "Levi", "Madison", "Nathan", "Oliver", "Piper", "Riley", "Samantha", "Tristan", "Valeria", "Wesley", "Xander", "Yara", "Zachary",
    "Alyssa", "Brandon", "Cody", "Dawn", "Emma", "Fiona", "Gabriel", "Hannah", "Iris", "Jack", "Keira", "Liam", "Nora", "Oscar", "Penny", "Ryan", "Sadie", "Tom", "Uma", "Violet", "Wyatt", "Xavier", "Yara", "Zeke",
    "Andrew", "Brooke", "Carter", "Daisy", "Ella", "Frank", "Grayson", "Harley", "Isla", "Jonah", "Kendall", "Lucas", "Mia", "Nolan", "Owen", "Paige", "Riley", "Sophia", "Toby", "Vera", "Wendy", "Xander", "Yara", "Zara",
    "Ash", "Bethany", "Caleb", "Daniel", "Eve", "Felix", "Georgia", "Hazel", "Ivy", "Jackson", "Kaylee", "Luca", "Morgan", "Nina", "Olivia", "Parker", "Ruby", "Sadie", "Tommy", "Vivian", "Will", "Xander", "Yasmin", "Zane",
    "Aidan", "Bianca", "Cameron", "David", "Ella", "Finn", "Gavin", "Harper", "Isabella", "Jayden", "Kira", "Logan", "Mason", "Nathaniel", "Olive", "Peter", "Quinn", "Sophie", "Trent", "Victoria", "Wyatt", "Xander", "Yara", "Zoey",
    "Anna", "Brock", "Charlotte", "Devon", "Evelyn", "Felix", "Grace", "Hannah", "Isaac", "Jack", "Kaitlyn", "Levi", "Mila", "Nicholas", "Olivia", "Paul", "Rachel", "Sienna", "Tom", "Violet", "Willow", "Xander", "Yasmine", "Zane",
    "Asher", "Brielle", "Cole", "Drew", "Elena", "Finn", "Gina", "Henry", "Isla", "James", "Kyle", "Logan", "Maya", "Nathaniel", "Olive", "Peyton", "Riley", "Stella", "Travis", "Veronica", "Weston", "Xander", "Yara", "Zara",
    "Alicia", "Bradley", "Chloe", "Damon", "Emily", "Freddy", "Grace", "Henry", "Isla", "Jake", "Kayla", "Leo", "Micah", "Nolan", "Olivia", "Paige", "Quincy", "Sarah", "Tyler", "Violet", "Will", "Xavier", "Yara", "Zane",
    "Arthur", "Bailey", "Connor", "Dean", "Eva", "Finn", "Grant", "Hazel", "Isaiah", "Jack", "Kylie", "Landon", "Molly", "Noah", "Opal", "Peyton", "Riley", "Tara", "Vivian", "Will", "Xena", "Yara", "Zoey",
    "Avery", "Brayden", "Caden", "Dylan", "Ella", "Finn", "Gina", "Henry", "Isaac", "Jackson", "Kara", "Levi", "Madison", "Nora", "Olive", "Piper", "Quinn", "Savannah", "Tommy", "Violet", "Will", "Xavier", "Yara", "Zane",
    "Audrey", "Blake", "Carson", "David", "Evelyn", "Fiona", "Grant", "Harper", "Ian", "James", "Kelsey", "Landon", "Mia", "Nathan", "Olivia", "Peyton", "Reed", "Tara", "Victor", "Wyatt", "Xander", "Yara", "Zoey",
    "Aiden", "Bella", "Caleb", "Derek", "Eva", "Finn", "George", "Harper", "Isaac", "Jacob", "Kira", "Logan", "Madison", "Nathaniel", "Olive", "Paige", "Quinn", "Sophie", "Trevor", "Veronica", "Willow", "Xander", "Yara", "Zane",
    "Aaron", "Brooke", "Cody", "Devon", "Elena", "Finn", "Grace", "Harper", "Isaac", "Jason", "Kylie", "Lucas", "Mia", "Noah", "Opal", "Peyton", "Quincy", "Ryan", "Sarah", "Trent", "Victoria", "Will", "Xander", "Yara", "Zoey",
    "Adam", "Brittany", "Cole", "Diana", "Emma", "Freddy", "Grace", "Hazel", "Ian", "James", "Kyle", "Liam", "Madison", "Noah", "Olivia", "Piper", "Quinn", "Samantha" };
    //Debug
    public Bibli _father, _mother;
    public Tile _tile;

    private void Start()
    {
        CreateBibli(_father, _mother, _tile);

    }
    void CreateBibli(Bibli father, Bibli mother, Tile tile)
    {
        Debug.Log("Criando novo Bibli");
        _instantiatePosition = new Vector3(tile.transform.position.x, tile.transform.position.y + 1, tile.transform.position.z);
        _instantiateRotation = new Quaternion(0, mother.transform.rotation.y + father.transform.rotation.y, 0, 1);
        GameObject bibli = Instantiate(bibliPrefab, _instantiatePosition, _instantiateRotation, playerPopulation.transform);
        Bibli newBibiliComponents = bibli.GetComponent<Bibli>();
        //
        newBibiliComponents.SetName(BibliNameCreator());
        newBibiliComponents.SetIdealHumidity(2 * father.GetIdealHumidity() + 2 * mother.GetIdealHumidity() + tile.GetHumidity() / 5);
        newBibiliComponents.SetIdealTemperature(2 * father.GetIdealTemperature() + 2 * mother.GetIdealTemperature() + tile.GetTemperature() / 5);
        bool _parenteDeMenorNivel = father.GetCurrentBibliLevel() < mother.GetCurrentBibliLevel() ? true : false;
        if (_parenteDeMenorNivel == true)
        {
            newBibiliComponents.SetCurrentBibliLevelStatus(father.GetCurrentBibliLevel());
        }
        else if (_parenteDeMenorNivel == false)
        {
            newBibiliComponents.SetCurrentBibliLevelStatus(mother.GetCurrentBibliLevel());
        }
        //Voltar depois
        newBibiliComponents.SetWeight(11);
        newBibiliComponents.SetHeight(11);
        newBibiliComponents.SetTemperature(tile.GetTemperature());
        newBibiliComponents.SetHealth(100);
        newBibiliComponents.SetDietTendency((father.GetDietTendency() + mother.GetDietTendency()) / 2);
        newBibiliComponents.SetFertilityBar(0);
        newBibiliComponents.SetCurrentBibliDiet();

        //Fim do voltar depois
        newBibiliComponents.DietaUpdater();
        newBibiliComponents.BibliStatusUpdater();
    }

    string BibliNameCreator()
    {
        System.Random random = new System.Random();
        string _nome;
        _nome = names[random.Next(names.Length)];
        return _nome;
    }

    private string ReadSpecificLine(string path, int lineNumber)
    {
        // Verifica se o arquivo existe
        if (!File.Exists(path))
        {
            Debug.LogError("Arquivo não encontrado.");
            return null;
        }

        using (StreamReader reader = new StreamReader(path))
        {
            string line;
            int currentLine = 1;

            // Lê o arquivo linha por linha
            while ((line = reader.ReadLine()) != null)
            {
                if (currentLine == lineNumber)
                {
                    return line; // Retorna a linha desejada
                }
                currentLine++;
            }
        }

        // Retorna null se a linha não existir
        return null;
    }
}