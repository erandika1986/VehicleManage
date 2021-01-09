import { Injectable } from '@angular/core';
import { FormControl } from '@angular/forms';
import { VehicleService } from '../services/vehicle/vehicle.service';
import { resolve } from 'url';

@Injectable()
export class VahicleRegNoValidator {
    debouncer: any;
    constructor(public vehcileService: VehicleService) {

    }

    checkVehicleRegNo(control: FormControl): any {
        clearTimeout(this.debouncer);

        return new Promise(resolve => {

            this.debouncer = setTimeout(() => {

                this.vehcileService.isVehicleAlreadyExists(control.value).subscribe((response) => {
                    if (!response.isSuccess) {
                        resolve(null);
                    }
                    else {
                        resolve({ 'vehicleNoInUse': true });
                    }
                })

            }, 1000);

        });
    }
}