using Class_C4U_test;



class Program
{
    static void Main()
    {
        Recepty newRecepty = new Recepty();
        newRecepty.Name = "Makkara Perunat";
        newRecepty.Instructions = "Paista perunoita 5min. Lisää sipulit ja makkarat. Paista kunnes perunat ja makkarat ovat kypsiä. Lisää mausteet oman maun mukaan.";
        newRecepty.Servings = 5;

        Recepty.Ingridiense Peruna = new Recepty.Ingridiense();
        Peruna.Name = "Peruna";
        Peruna.Grams = 500;
        newRecepty.Ingridienses.Add( Peruna );

        Recepty.Ingridiense Makkara = new Recepty.Ingridiense();
        Makkara.Name = "Makkara";
        Makkara.Grams = 400;
        newRecepty.Ingridienses.Add(Makkara);

        Recepty.Ingridiense Sipuli = new Recepty.Ingridiense();
        Sipuli.Name = "Sipuli";
        Sipuli.Grams = 100;
        newRecepty.Ingridienses.Add(Sipuli);

        //Testi alapuolella toimiiko else.

        //Recepty.Ingridiense Suola = new Recepty.Ingridiense();
        //Suola.Name = "Suola";
        //Suola.Grams = 5;
        //newRecepty.Ingridienses.Add(Suola);

        List<IngridienseCalories> CaloryList = new List<IngridienseCalories>();

        IngridienseCalories peruna = new IngridienseCalories();
        peruna.Name = "Peruna";
        peruna.Calories = 100;
        CaloryList.Add( peruna );
        IngridienseCalories.SaveJson(peruna);

        IngridienseCalories makkara = new IngridienseCalories();
        makkara.Name = "Makkara";
        makkara.Calories = 200;
        CaloryList.Add(makkara);
        IngridienseCalories.SaveJson(makkara);

        IngridienseCalories sipuli = new IngridienseCalories();
        sipuli.Name = "Sipuli";
        sipuli.Calories = 50;
        CaloryList.Add(sipuli);
        IngridienseCalories.SaveJson(sipuli);

        Recepty.TulostaResepti(newRecepty, CaloryList);
        Recepty.SaveJson(newRecepty);
        string hakuSana = "Makkara Perunat";
        Recepty.LataaResepti(hakuSana);


    }
}
