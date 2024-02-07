using Class_C4U_test;



class Program
{
    static void Main()
    {
        
        string name = "Makkara Perunat";
        string instructions = "Paista perunoita 5min. Lisää sipulit ja makkarat. Paista kunnes perunat ja makkarat ovat kypsiä. Lisää mausteet oman maun mukaan.";
        int servings = 5;
        Recepty newRecepty = new Recepty(name, instructions, servings);

        name = "Peruna";
        int grams = 500;
        Recepty.Ingridiense Peruna = new Recepty.Ingridiense(name, grams);
        newRecepty.Ingridienses.Add( Peruna );

        name = "Makkara";
        grams = 400;
        Recepty.Ingridiense Makkara = new Recepty.Ingridiense(name, grams);
        newRecepty.Ingridienses.Add(Makkara);

        name = "Sipuli";
        grams = 100;
        Recepty.Ingridiense Sipuli = new Recepty.Ingridiense(name, grams);
        newRecepty.Ingridienses.Add(Sipuli);

        //Testi alapuolella toimiiko else.

        //Recepty.Ingridiense Suola = new Recepty.Ingridiense();
        //Suola.Name = "Suola";
        //Suola.Grams = 5;
        //newRecepty.Ingridienses.Add(Suola);

        List<IngridienseCalories> CaloryList = new List<IngridienseCalories>();

        name = "Peruna";
        double calories = 100;
        IngridienseCalories peruna = new IngridienseCalories(name, calories);
        IngridienseCalories.SaveJson(peruna);
        CaloryList.Add(peruna);

        name = "Makkara";
        calories = 200;
        IngridienseCalories makkara = new IngridienseCalories(name, calories);
        IngridienseCalories.SaveJson(makkara);
        CaloryList.Add(makkara);

        name = "Sipuli";
        calories = 50;
        IngridienseCalories sipuli = new IngridienseCalories(name, calories);
        IngridienseCalories.SaveJson(sipuli);
        CaloryList.Add(sipuli);

        Recepty.TulostaResepti(newRecepty, CaloryList);
        Recepty.SaveJson(newRecepty);
        string hakuSana = "Makkara Perunat";
        Recepty.LataaResepti(hakuSana);


    }
}
