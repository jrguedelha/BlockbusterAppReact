import React, {Component} from 'react';
import { Modal, Button, Row, Col, Form } from 'react-bootstrap';

export class AddMovie extends Component{
    constructor(props){
        super(props);
        this.handleSubmit = this.handleSubmit.bind(this);
    }

    handleSubmit(event){
        event.preventDefault();
        fetch(process.env.REACT_APP_API+'movie', {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type':'application/json'
            },
            body: JSON.stringify({
                Id: null,
                Title: event.target.Title.value,
            //    ReleaseDate: event.target.ReleaseDate.value,
            //    IsActive: event.target.IsActive.value,
            //    Genre: event.target.Title.value,
            })
        })
        .then(res => res.json())
        .then((result) => {
            alert(result);
        },
        (error) => {
            alert('Failed');
        })
    }

    render(){
        return (
            <div className="container">
                <Modal {...this.props}
                    size="lg"
                    aria-labelledby = "contained-movie-title-vcenter"
                    centered
                >
                    <Modal.Header clooseButton>
                        <Modal.Title id = "contained-movie-title-vcenter">
                                Add Movie
                        </Modal.Title>
                    </Modal.Header>

                    <Modal.Body>
                        <Row>
                            <Col sm={6}>
                                <Form onSubmit = {this.handleSubmit}>
                                    <Form.Group controlId="Title">
                                        <Form.Label>Title</Form.Label>
                                        <Form.Control type="text" name="Title"
                                            required
                                            placeholder="Title"
                                        />
                                    </Form.Group>

                                    <Form.Group>
                                        <Button variant = "primary" type="submit">
                                            Adicionar
                                        </Button>
                                    </Form.Group>
                                </Form>
                            </Col>
                        </Row>
                    </Modal.Body>
                    
                   <Modal.Footer>
                        <Button variant = "danger" onClick = {this.props.onHide}>
                            Fechar
                        </Button>
                    </Modal.Footer>
                </Modal>
            </div>
        )
    }
}