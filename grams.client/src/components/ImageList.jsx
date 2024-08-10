import React, { useEffect, useState } from 'react';
import axios from 'axios';

const ImageList = () => {
    const [images, setImages] = useState([]);

    const fetchImages = async () => {
        try {
            const response = await axios.get('https://localhost:7099/api/image');
            setImages(response.data.data);
        } catch (error) {
            console.error('Error fetching images:', error);
        }
    };

    useEffect(() => {
         fetchImages();
    }, []);

    return (
        <div>
            {images.length > 0 ? (
                <div>
                    {images.map((image) => (
                        <div key={image.id}>
                            <img
                                src={`https://localhost:7099/uploads/${image.fileName}`}
                                alt={image.fileName}
                                style={{ width: '500px', height: 'auto' }}
                            />
                            <p>{image.fileName}</p>
                        </div>
                    ))}
                </div>
            ) : (
                <p>No images available.</p>
            )}
        </div>
    );
};

export default ImageList;
