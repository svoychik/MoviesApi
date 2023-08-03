#!/bin/bash

# Your AWS region
AWS_REGION="eu-central-1"

# Your DynamoDB table name
TABLE_NAME="Movies"

# Sample movie data in JSON format
MOVIES_DATA='[
    {
        "Id": "1",
        "Title": "The Shawshank Redemption",
        "Description": "Two imprisoned men bond over several years, finding solace and eventual redemption through acts of common decency.",
        "ReleaseDate": "1994-09-23",
        "Genre": "Drama",
        "Rating": 9.3
    },
    {
        "Id": "2",
        "Title": "The Godfather",
        "Description": "The aging patriarch of an organized crime dynasty transfers control of his clandestine empire to his reluctant son.",
        "ReleaseDate": "1972-03-24",
        "Genre": "Crime, Drama",
        "Rating": 9.2
    },
    {
        "Id": "3",
        "Title": "The Dark Knight",
        "Description": "When the menace known as the Joker wreaks havoc and chaos on the people of Gotham, Batman must accept one of the greatest psychological and physical tests of his ability to fight injustice.",
        "ReleaseDate": "2008-07-18",
        "Genre": "Action, Crime, Drama",
        "Rating": 9.0
    },
    {
        "Id": "4",
        "Title": "Pulp Fiction",
        "Description": "The lives of two mob hitmen, a boxer, a gangsters wife, and a pair of diner bandits intertwine in four tales of violence and redemption.",
        "ReleaseDate": "1994-10-14",
        "Genre": "Crime, Drama",
        "Rating": 8.9
    },
    {
        "Id": "5",
        "Title": "Forrest Gump",
        "Description": "The presidencies of Kennedy and Johnson, the events of Vietnam, Watergate, and other historical events unfold through the perspective of an Alabama man with an IQ of 75.",
        "ReleaseDate": "1994-07-06",
        "Genre": "Drama, Romance",
        "Rating": 8.8
    },
    {
        "Id": "6",
        "Title": "The Matrix",
        "Description": "A computer hacker learns from mysterious rebels about the true nature of his reality and his role in the war against its controllers.",
        "ReleaseDate": "1999-03-31",
        "Genre": "Action, Sci-Fi",
        "Rating": 8.7
    },
    {
        "Id": "7",
        "Title": "Inception",
        "Description": "A thief who steals corporate secrets through the use of dream-sharing technology is given the inverse task of planting an idea into the mind of a C.E.O.",
        "ReleaseDate": "2010-07-16",
        "Genre": "Action, Adventure, Sci-Fi",
        "Rating": 8.8
    },
    {
        "Id": "8",
        "Title": "The Lion King",
        "Description": "Lion prince Simba and his father are targeted by his bitter uncle, who wants to ascend the throne himself.",
        "ReleaseDate": "1994-06-15",
        "Genre": "Animation, Adventure, Drama",
        "Rating": 8.5
    },
    {
        "Id": "9",
        "Title": "Gladiator",
        "Description": "A former Roman General sets out to exact vengeance against the corrupt emperor who murdered his family and sent him into slavery.",
        "ReleaseDate": "2000-05-01",
        "Genre": "Action, Adventure, Drama",
        "Rating": 8.5
    },
    {
        "Id": "10",
        "Title": "The Lord of the Rings: The Return of the King",
        "Description": "Gandalf and Aragorn lead the World of Men against Saurons army to draw his gaze from Frodo and Sam as they approach Mount Doom with the One Ring.",
        "ReleaseDate": "2003-12-17",
        "Genre": "Action, Adventure, Drama",
        "Rating": 8.9
    },
    {
        "Id": "11",
        "Title": "The Departed",
        "Description": "An undercover cop and a mole in the police attempt to identify each other while infiltrating an Irish gang in South Boston.",
        "ReleaseDate": "2006-10-06",
        "Genre": "Crime, Drama, Thriller",
        "Rating": 8.5
    },
    {
        "Id": "12",
        "Title": "The Prestige",
        "Description": "After a tragic accident, two stage magicians engage in a battle to create the ultimate illusion while sacrificing everything they have to outwit each other.",
        "ReleaseDate": "2006-10-20",
        "Genre": "Drama, Mystery, Sci-Fi",
        "Rating": 8.5
    },
    {
        "Id": "13",
        "Title": "The Avengers",
        "Description": "Earths mightiest heroes must come together and learn to fight as a team if they are going to stop the mischievous Loki and his alien army from enslaving humanity.",
        "ReleaseDate": "2012-05-04",
        "Genre": "Action, Adventure, Sci-Fi",
        "Rating": 8.0
    },
    {
        "Id": "14",
        "Title": "Interstellar",
        "Description": "A team of explorers travel through a wormhole in space in an attempt to ensure humanitys survival.",
        "ReleaseDate": "2014-11-07",
        "Genre": "Adventure, Drama, Sci-Fi",
        "Rating": 8.6
    },
    {
        "Id": "15",
        "Title": "Jurassic Park",
        "Description": "A pragmatic paleontologist visiting an almost complete theme park is tasked with protecting a couple of kids after a power failure causes the parks cloned dinosaurs to run loose.",
        "ReleaseDate": "1993-06-11",
        "Genre": "Adventure, Sci-Fi",
        "Rating": 8.1
    }
    # Add more movie objects here...
]'

# Function to insert movie data into DynamoDB
function seed_movies {
    echo "Seeding DynamoDB with movies..."
    aws dynamodb batch-write-item --region $AWS_REGION --request-items "$MOVIES_DATA" --return-consumed-capacity TOTAL
    echo "Seeding completed."
}

# Main script
seed_movies
