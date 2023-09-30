<script>
    let autocomplete;
    function initAutocomplete() {
        autocomplete = new google.maps.places.Autocomplete(
            document.getElementById('autocomplete'),
            {
                type: ['establishment'],
                //componentRestrictions: { 'country': ['AU'] }, enable global
                fields: ['place_id', 'geometry', 'name', 'photos', 'rating', 'user_ratings_total', 'website']
            });

        autocomplete.addListener("place_changed", onPlaceChanged);
    }
    

    document.getElementById("autocomplete").addEventListener("keydown", function (event) {
        if (event.key === "Enter") {
            event.preventDefault();
            fetchPlaceDetails();
        }
    });

    function fetchPlaceDetails() {
        const inputValue = document.getElementById("autocomplete").value;
        if (inputValue) {
            const placesService = new google.maps.places.PlacesService(document.createElement('div'));
            placesService.textSearch({ query: inputValue }, function (results, status) {
                if (status === google.maps.places.PlacesServiceStatus.OK && results[0]) {
                    const request = {
                        placeId: results[0].place_id,
                        fields: ["name", "photos", "rating", "user_ratings_total", "website"]
                    };

                    placesService.getDetails(request, function (place, status) {
                        if (status === google.maps.places.PlacesServiceStatus.OK) {
                            displayPhotos(place);
                        } else {
                            document.getElementById('photo-container').innerText("No Suggestions Available");
                        }
                    });
                }
            });
        }
    }

    function onPlaceChanged() {
        const place = autocomplete.getPlace();

        // Clear the previous photos if any
        const photoContainer = document.getElementById('photo-container');
        photoContainer.innerHTML = '';

        if (place.photos && place.photos.length > 0) {
            place.photos.forEach((photo) => {
                appendImageAndButton(photo, photoContainer);
            });
        }
    }

    function displayPhotos(place) {
        const photoContainer = document.getElementById('photo-container');
        photoContainer.innerHTML = '';

        if (place.photos && place.photos.length > 0) {
            place.photos.forEach((photo) => {
                appendImageAndButton(photo, photoContainer);
            });
        }
    }

    function appendImageAndButton(photo, photoContainer) {
        const imageDiv = document.createElement('div');
        imageDiv.className = 'image-wrapper';

        const photoUrl = photo.getUrl();
        const imgElement = document.createElement('img');
        imgElement.src = photoUrl;
        imgElement.alt = 'Loading...';

        const viewMoreButton = document.createElement('button');
        viewMoreButton.innerText = 'View More';
        viewMoreButton.onclick = function () {
            alert("clicked");
        };

        imageDiv.appendChild(imgElement);
        imageDiv.appendChild(viewMoreButton);

        photoContainer.appendChild(imageDiv);
    }
</script>