import { Component, Inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { CarService } from '../service/car.service';

const createFormGroup = dataItem => new FormGroup({
    'id': new FormControl(dataItem.id),
    'brand': new FormControl(dataItem.brand, Validators.required),
    'type': new FormControl(dataItem.type, Validators.required),
    'price': new FormControl(dataItem.price),
});

@Component({
    selector: 'app-home',
    templateUrl: './home.component.html',
})
export class HomeComponent {
    public gridData: any[];
    public formGroup: FormGroup;
    private editedRowIndex: number;

    constructor(private carService: CarService) {
    }

    public ngOnInit(): void {
        this.getData();
    }

    public getData()
    {
        this.carService.getCars()
            .subscribe(result => {
                this.gridData = result;
            }, error =>{
                console.log(error);
            })
    }

    public addHandler({ sender }) {
        this.closeEditor(sender);

        this.formGroup = createFormGroup({
            'brand': '',
            'type': '',
            'price': 0
        });

        sender.addRow(this.formGroup);
    }

    public editHandler({ sender, rowIndex, dataItem }) {
        this.closeEditor(sender);

        this.formGroup = createFormGroup(dataItem);

        this.editedRowIndex = rowIndex;
        sender.editRow(rowIndex, this.formGroup);

    }

    public cancelHandler({ sender, rowIndex }) {
        this.closeEditor(sender, rowIndex);
    }

    public saveHandler({ sender, rowIndex, formGroup,isNew}): void {
        const car = formGroup.value;

        if(isNew)
        {
            car.id = 0;
            this.carService.Create(car)
            .subscribe(result => {
                console.log(result),
                this.getData();
            }, error =>{
                console.log(error);
            })
        }
        else
        {
            this.carService.Update(car)
            .subscribe(result => {
                console.log(result)
                Object.assign(
                    this.gridData.find(({ id }) => id === car.id),
                    car)
            }, error =>{
                console.log(error);
            })
        }
        

        sender.closeRow(rowIndex);
    }

    public removeHandler({ dataItem }): void {
        this.carService.Delete(dataItem.id)
        .subscribe(result => {
            console.log(result),
            this.getData();
        }, error =>{
            console.log(error);
        })
    }

    private closeEditor(grid, rowIndex = this.editedRowIndex) {
        grid.closeRow(rowIndex);
        this.editedRowIndex = undefined;
        this.formGroup = undefined;
    }
}
