import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export abstract class BaseService {
  constructor() {}

  protected handleError(error: any) {
    var applicationError = error.headers.get('Application-Error');

    if (applicationError) {
      return new Error(applicationError);
    }

    var modelStateErrors: string | null = '';

    for (var key in error.error) {
      if (error.error[key])
        modelStateErrors += error.error[key].description + '\n';
    }

    //return;
    modelStateErrors = modelStateErrors === '' ? null : modelStateErrors;
    return new Error(modelStateErrors || 'Server error');
  }
}
