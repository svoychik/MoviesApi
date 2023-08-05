const AWS = require('aws-sdk');

const AWS_REGION = 'eu-central-1';
const TABLE_NAME = 'Movies';
AWS.config.update({ region: AWS_REGION });

const items = [{
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
}];

// Create a DynamoDB document client
const dynamodb = new AWS.DynamoDB.DocumentClient();

// Prepare batch write requests
const putRequests = items.map(item => ({
  PutRequest: {
    Item: item
  }
}));

const params = {
  RequestItems: {
    [TABLE_NAME]: putRequests
  }
};

// Execute the batch write operation
dynamodb.batchWrite(params, (error, data) => {
  if (error) {
    console.error('Error:', error);
  } else {
    console.log('Batch write operation successful:', data);
  }
});
