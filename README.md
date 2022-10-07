# Hello Barcode Scanner REST API

A minimal application which uses [Accusoft's Barcode Scanner REST API](https://help.accusoft.com/BarcodeXpress/latest/BxNetCore/webframe.html#rest-api.html)
to detect barcodes on a given bitmap image.

## API Key

In order to utilize the Barcode Scanner REST API, you will first need
an [Accusoft Cloud Account](https://cloud.accusoft.com/).

Once you have registered for a paid or trial cloud account, you can retrieve
your key from the [API Key](https://cloud.accusoft.com/account/apikey) page.
Then, either place it in `HelloBarcodeScanner.cs` on line 11, replacing the
string: `YourAPIKeyHere...`, or use it to set the ACCUSOFT_CLOUD_KEY
environment variable.

## Running the Samples

The following command is sufficient to both build and run the sample:

    dotnet run

The sample should upload the test image to Accusoft's Work File Service,
decode it with the Barcode Scanner REST API, and then produce output similar to
this:

    {
      "processId": "fWGBrkSFkHzyoYGl7uA8ag",
      "state": "complete",
      "expirationDateTime": "2022-10-05T14:03:44.000Z",
      "input": {
        "source": {
          "fileId": "b6QwpPhmAaCVzvqFb5vriw"
        },
        "barcodeTypes": [
          "code39Barcode"
        ]
      },
      "output": {
        "barcodes": [
          {
            "area": {
              "x": 1992,
              "y": 829,
              "width": 426,
              "height": 128
            },
            "barcodeType": "code39Barcode",
            "barcodeDataAsBytes": "VjNUWDVOAA==",
            "barcodeValue": "V3TX5N",
            "confidence": 100,
            "numberOfCheckSumChars": 0,
            "point1": {
              "x": 1992,
              "y": 829
            },
            "point2": {
              "x": 2418,
              "y": 829
            },
            "point3": {
              "x": 2418,
              "y": 957
            },
            "point4": {
              "x": 1992,
              "y": 957
            },
            "skew": 0,
            "validCheckSum": false
          },
          {
            "area": {
              "x": 160,
              "y": 2324,
              "width": 349,
              "height": 84
    ...
