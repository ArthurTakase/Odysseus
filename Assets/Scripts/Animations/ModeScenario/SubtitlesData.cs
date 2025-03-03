using System.Collections.Generic;

public class SubtitlesData
{
    // Step 1
    private readonly Subtitles STR_STEP1_1 = new("La Lune...", 1.5f);
    private readonly Subtitles STR_STEP1_2 = new("Ce fidèle compagnon de la Terre", 2f);
    private readonly Subtitles STR_STEP1_3 = new("traverse inlassablement les mêmes phases depuis\ndes millénaires.", 3.5f);
    private readonly Subtitles STR_STEP1_4 = new("Mais parfois, au cours de son voyage,", 3f);
    private readonly Subtitles STR_STEP1_5 = new("elle nous offre des spectacles rares et\nfascinants.", 3f);
    private readonly Subtitles STR_STEP1_6 = new("Regardons ça de plus près...", 2.5f);

    // Step 2
    private readonly Subtitles STR_STEP2_1 = new("Première arrêt, la Lune Bleue.", 2f);
    private readonly Subtitles STR_STEP2_2 = new("Elle ne doit pas son nom à sa couleur.", 3.5f);
    private readonly Subtitles STR_STEP2_3 = new("Il s'agit en réalité de la deuxième\npleine lune d'un même mois.", 3.5f);
    private readonly Subtitles STR_STEP2_4 = new("Un événement peu fréquent, qui ne se produit\nqu'une fois tous les deux à trois ans.", 4.5f);
    private readonly Subtitles STR_STEP2_5 = new("Dans l'imaginaire populaire,", 2f);
    private readonly Subtitles STR_STEP2_6 = new("la Lune bleue symbolise ce qui est rare,\nce qui dépasse les normes.", 4f);

    // Step 3
    private readonly Subtitles STR_STEP3_1 = new("Avançons jusqu'à la Lune de sang,\négalement appelée \"éclipse lunaire totale\".", 5f);
    private readonly Subtitles STR_STEP3_2 = new("Au lieu de disparaître dans l'obscurité,", 2.5f);
    private readonly Subtitles STR_STEP3_3 = new("la Lune revêt une robe rougeâtre.", 2.5f);
    private readonly Subtitles STR_STEP3_4 = new("Ce phénomène se produit lorsque\nla lumière du Soleil,", 3.5f);
    private readonly Subtitles STR_STEP3_5 = new("filtrée par l'atmosphère terrestre,", 2f);
    private readonly Subtitles STR_STEP3_6 = new("éclaire la Lune d'une lueur étrange,\npresque surnaturelle.", 4.5f);

    // Step 4
    private readonly Subtitles STR_STEP4_1 = new("Les éclipses solaires sont une autre danse céleste,", 2.5f);
    private readonly Subtitles STR_STEP4_2 = new("mais cette fois, c'est la Lune qui s'interpose\nentre nous et le Soleil.", 4.5f);
    private readonly Subtitles STR_STEP4_3 = new("Lors d'une éclipse solaire totale,", 2.5f);
    private readonly Subtitles STR_STEP4_4 = new("elle parvient à masquer complètement le disque\nsolaire,", 2.5f);
    private readonly Subtitles STR_STEP4_5 = new("plongeant des régions entières de la Terre\n dans l'obscurité en plein jour.", 4f);
    private readonly Subtitles STR_STEP4_6 = new("C'est un moment rare et saisissant,", 2.5f);
    private readonly Subtitles STR_STEP4_7 = new("lorsque le jour devient nuit,", 1.5f);
    private readonly Subtitles STR_STEP4_8 = new("et que l'on peut observer la couronne solaire.", 3f);

    // Step 5
    private readonly Subtitles STR_STEP5_1 = new("La majorité des événements lunaires résultent\nde la complexité des interactions entre\nla Terre,", 4.5f);
    private readonly Subtitles STR_STEP5_2 = new("la Lune et le Soleil.", 2.5f);
    private readonly Subtitles STR_STEP5_3 = new("Mais l'acteur principal de toute cette histoire,", 2.5f);
    private readonly Subtitles STR_STEP5_4 = new("n'est autre que l'inclinaison de l'axe terrestre.", 3f);
    private readonly Subtitles STR_STEP5_5 = new("En effet, les plans orbitaux de la Terre et\nde la Lune ne sont pas parfaitement\nparallèles,", 4.5f);
    private readonly Subtitles STR_STEP5_6 = new("ce qui permet à la lumière du Soleil de frapper\nla Lune sous différents angles,", 4f);
    private readonly Subtitles STR_STEP5_7 = new("générant ainsi les phases que nous observons\nchaque mois.", 2.5f);
    private readonly Subtitles STR_STEP5_8 = new("Cette inclinaison est également responsable des\nspectacles exceptionnels que nous avons\npu voir aujourd'hui,", 6f);
    private readonly Subtitles STR_STEP5_9 = new("et bien d'autres merveilles célestes qui\ncaptivent notre regard.", 3.5f);

    // BluedMoon
    private readonly Subtitles STR_BLUEMOON_1 = new("La Lune Bleue est un phénomène rare qui survient\ntous les deux à trois ans.", 4f);
    private readonly Subtitles STR_BLUEMOON_2 = new("Elle désigne en général la deuxième pleine lune\nd'un même mois.", 3.5f);
    private readonly Subtitles STR_BLUEMOON_3 = new("Malgré son nom, la Lune ne change pas de couleur,\nsauf dans de rares conditions atmosphériques.", 6.5f);
    private readonly Subtitles STR_BLUEMOON_4 = new("L'expression vient d'une mauvaise interprétation\nanglaise de \"Blue Moon\", qui signifie\nsimplement \"très rare\".", 6f);
    private readonly Subtitles STR_BLUEMOON_5 = new("Astronomiquement, elle se produit lorsque la Terre\nest alignée entre le Soleil et la Lune,\ncomme pour une pleine lune classique.", 7f);

    // BloodMoon
    private readonly Subtitles STR_BLOODMOON_1 = new("La Lune de Sang se produit lors d'une éclipse\nlunaire totale, quand la Terre est alignée\nentre le Soleil et la Lune.", 6f);
    private readonly Subtitles STR_BLOODMOON_2 = new("La Lune apparaît rougeâtre à cause de la\ndiffusion de Rayleigh, le même phénomène qui\ncolore les couchers de soleil.", 6.5f);
    private readonly Subtitles STR_BLOODMOON_3 = new("Ces éclipses, représentant 35 % des éclipses\nlunaires, surviennent environ tous les\ndeux ans et demi à un endroit donné.", 8f);
    private readonly Subtitles STR_BLOODMOON_4 = new("Leur teinte varie selon la quantité de particules\ndans l'atmosphère.", 4f);
    private readonly Subtitles STR_BLOODMOON_5 = new("Elles suivent le cycle de Saros, un cycle de\n18 ans permettant de prévoir des éclipses\nsimilaires.", 7f);

    // Total Eclipse
    private readonly Subtitles STR_TOTALECLIPSE_1 = new("Une éclipse solaire totale se produit lorsque\nla Lune s'aligne entre la Terre et le Soleil,", 4f);
    private readonly Subtitles STR_TOTALECLIPSE_2 = new("bloquant la lumière dans une zone limitée.", 3.5f);
    private readonly Subtitles STR_TOTALECLIPSE_3 = new("Ces éclipses, visibles uniquement dans une bande\nétroite, surviennent tous les 18 mois quelque part\nsur Terre,", 6f);
    private readonly Subtitles STR_TOTALECLIPSE_4 = new("mais restent très rares à un endroit donné,\nenviron une fois tous les 300 à 400 ans.", 5f);
    private readonly Subtitles STR_TOTALECLIPSE_5 = new("Depuis l'Antiquité, elles fascinent les\ncivilisations,", 3f);
    private readonly Subtitles STR_TOTALECLIPSE_6 = new("comme en 585 av.J.-C., où une éclipse mit\nfin à une bataille en effrayant les\ncombattants.", 6f);

    public List<Subtitles> STEP1;
    public List<Subtitles> STEP2;
    public List<Subtitles> STEP3;
    public List<Subtitles> STEP4;
    public List<Subtitles> STEP5;
    public List<Subtitles> BLUEMOON;
    public List<Subtitles> BLOODMOON;
    public List<Subtitles> TOTALECLIPSE;

    public SubtitlesData()
    {
        STEP1 = new() { STR_STEP1_1, STR_STEP1_2, STR_STEP1_3, STR_STEP1_4, STR_STEP1_5, STR_STEP1_6 };
        STEP2 = new() { STR_STEP2_1, STR_STEP2_2, STR_STEP2_3, STR_STEP2_4, STR_STEP2_5, STR_STEP2_6 };
        STEP3 = new() { STR_STEP3_1, STR_STEP3_2, STR_STEP3_3, STR_STEP3_4, STR_STEP3_5, STR_STEP3_6 };
        STEP4 = new() { STR_STEP4_1, STR_STEP4_2, STR_STEP4_3, STR_STEP4_4, STR_STEP4_5, STR_STEP4_6, STR_STEP4_7, STR_STEP4_8 };
        STEP5 = new() { STR_STEP5_1, STR_STEP5_2, STR_STEP5_3, STR_STEP5_4, STR_STEP5_5, STR_STEP5_6, STR_STEP5_7, STR_STEP5_8, STR_STEP5_9 };
        BLUEMOON = new() { STR_BLUEMOON_1, STR_BLUEMOON_2, STR_BLUEMOON_3, STR_BLUEMOON_4, STR_BLUEMOON_5 };
        BLOODMOON = new() { STR_BLOODMOON_1, STR_BLOODMOON_2, STR_BLOODMOON_3, STR_BLOODMOON_4, STR_BLOODMOON_5 };
        TOTALECLIPSE = new() { STR_TOTALECLIPSE_1, STR_TOTALECLIPSE_2, STR_TOTALECLIPSE_3, STR_TOTALECLIPSE_4, STR_TOTALECLIPSE_5, STR_TOTALECLIPSE_6 };
    }
}